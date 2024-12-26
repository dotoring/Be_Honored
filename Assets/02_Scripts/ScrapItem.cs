using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScrapItem : MonoBehaviour
{
	XRGrabInteractable xRGrabInteractable;
	PhotonView pv;
	Rigidbody rb;
	Collider col;
	GameObject bag;
	bool isInBag;

	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		pv = GetComponent<PhotonView>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}

	private void Start()
	{
		ExitMgr.OnExitDungeon += DestroyPhotonView;

		xRGrabInteractable.selectEntered.AddListener((args) =>
		{
			//오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
			//TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
			pv.TransferOwnership(PhotonNetwork.LocalPlayer);

			col.isTrigger = true;

		});

		xRGrabInteractable.selectExited.AddListener((args) =>
		{
			col.isTrigger = false;
			CheckInBag();
		});

		App.Instance.interactorManager.AddListener((mgr) =>
		{
			SetInteractionMgr(mgr);
		});
	}

	private void OnDestroy()
	{
		if (App.Instance != null)
		{
			App.Instance.interactorManager.RemoveListener((mgr) =>
			{
				SetInteractionMgr(mgr);
			});
		}
	}

	void CheckInBag()
	{
		if (isInBag)
		{
			SetInBag();
		}
		else
		{
			PullOut();
		}
	}

	void SetInBag()
	{
		pv.RPC(nameof(SetItemActive), RpcTarget.OthersBuffered, false);

		rb.isKinematic = true;
		rb.useGravity = false;

		transform.SetParent(bag.transform, true);
	}

	void PullOut()
	{
		pv.RPC(nameof(SetItemActive), RpcTarget.OthersBuffered, true);

		rb.isKinematic = false;
		rb.useGravity = true;

		transform.SetParent(null, true);
	}

	[PunRPC]
	void SetItemActive(bool b)
	{
		gameObject.SetActive(b);
	}

	void DestroyPhotonView()
	{
		Destroy(pv);
	}

	public void SetInteractionMgr(XRInteractionManager mgr)
	{
		xRGrabInteractable.interactionManager = mgr;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = true;
			if (bag == null)
			{
				bag = other.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = false;
		}
	}
}
