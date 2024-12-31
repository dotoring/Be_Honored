using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public enum TypeOfItem
{
	EQUIP,
	SCRAP,
}

public class ScrapItem : MonoBehaviour
{
	[SerializeField] protected TypeOfItem typeOfItem = TypeOfItem.SCRAP;
	protected XRGrabInteractable xRGrabInteractable;
	protected PhotonView pv;
	protected Rigidbody rb;
	protected Collider col;
	protected GameObject bag;
	protected BagCtrl bagCtrl;
	protected bool isInBag;
	protected private int price = 1;
	public int weight = 5;

	protected Action<XRInteractionManager> action;

	public int Price { get => price; set => price = value; }

	protected void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		pv = GetComponent<PhotonView>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
	}

	protected virtual void Start()
	{
		ExitMgr.OnExitDungeon += DestroyPhotonView;
		action += SetInteractionMgr;

		xRGrabInteractable.selectEntered.AddListener((args) =>
		{
			//오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
			//TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
			if (pv != null || !SceneManager.GetActiveScene().name.Equals("lobbySample_Working1"))
			{
				pv.TransferOwnership(PhotonNetwork.LocalPlayer);
			}

			col.isTrigger = true;

		});

		xRGrabInteractable.selectExited.AddListener((args) =>
		{
			col.isTrigger = false;
			CheckInBag();
		});

		App.Instance.interactorManager.AddListener(action);

		//ItemSetter();
	}

	public virtual void ItemSetter(int value)
	{
		Price = value;
	}

	protected void OnDestroy()
	{
		if (App.Instance != null)
		{
			App.Instance.interactorManager.RemoveListener(action);
		}
	}

	protected virtual void CheckInBag()
	{
		if (isInBag)
		{
			if (bagCtrl.CheckWeight(weight))
			{
				SetInBag();
				bagCtrl.AddScrap(this);
			}
		}
		else
		{
			PullOut();
			bagCtrl.RemoveScrap(this);
		}
	}

	protected void SetInBag()
	{
		if (pv != null || !SceneManager.GetActiveScene().name.Equals("lobbySample_Working1"))
		{
			pv.RPC(nameof(SetItemActive), RpcTarget.OthersBuffered, false);
		}

		xRGrabInteractable.throwOnDetach = false;
		rb.isKinematic = true;
		rb.useGravity = false;

		transform.SetParent(bag.transform, true);
	}

	protected void PullOut()
	{
		if (pv != null || !SceneManager.GetActiveScene().name.Equals("lobbySample_Working1"))
		{
			pv.RPC(nameof(SetItemActive), RpcTarget.OthersBuffered, true);
		}

		xRGrabInteractable.throwOnDetach = true;
		rb.isKinematic = false;
		rb.useGravity = true;

		transform.SetParent(null, true);
	}

	[PunRPC]
	protected void SetItemActive(bool b)
	{
		gameObject.SetActive(b);
	}

	protected void DestroyPhotonView()
	{
		Destroy(pv);
		Destroy(GetComponent<PhotonTransformView>());
	}

	public void SetInteractionMgr(XRInteractionManager mgr)
	{
		xRGrabInteractable.interactionManager = mgr;
	}

	protected void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = true;
			if (bag == null)
			{
				bag = other.gameObject;
				bagCtrl = bag.GetComponent<BagCtrl>();
			}
		}
	}

	protected void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = false;
		}
	}

	public int Getvalue()
	{
		return Price;
	}
}
