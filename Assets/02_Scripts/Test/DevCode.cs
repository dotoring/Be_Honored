using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DevCode : MonoBehaviour
{
	public InputActionProperty xrprimary;
	public PhotonManager photon;

	private void Awake()
	{
		xrprimary.action.performed += clickA;
	}

	private void clickA(InputAction.CallbackContext context)
	{
		photon.MakeRoomBtnOnClick(RoomLevel.One);
	}
}
