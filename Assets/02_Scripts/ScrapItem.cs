using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Photon;
using Photon.Pun;

public class ScrapItem : MonoBehaviour
{
	XRGrabInteractable xRGrabInteractable;
	PhotonView pv;

	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		pv = GetComponent<PhotonView>();
	}

	private void Start()
	{

		xRGrabInteractable.selectEntered.AddListener(grabed);

		if (xRGrabInteractable != null)
		{
			xRGrabInteractable.selectEntered.AddListener((args) =>
			{
				//오브젝트의 PhotonView에서 Ownership Transfer를 Takeover로 설정하면 소유권(컨트롤러 포함)을 강제로 가져올 수 있도록 한다
				//TransferOwnership(Player) -> 현재 PhotonView의 소유권을 Player로 바꾸는 함수
				pv.TransferOwnership(PhotonNetwork.LocalPlayer);
			});
		}
	}



	private void grabed(SelectEnterEventArgs arg0)
	{
		Debug.Log($" selected ");
		App.Instance.inventory.Add(this.name);
		Destroy(this.gameObject);
	}
}
