using Photon.Pun;
using Suntail;
using System;
using System.Collections;
using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
	[SerializeField] GameObject door;
	bool isOpen;

	public Action OnDoorOpen = () => { };

    public void OpenDoor()
	{
		if (!isOpen)
		{
			GetComponent<PhotonView>().RPC(nameof(OpenCoroutine), RpcTarget.AllBuffered);
			GetComponent<PhotonView>().RPC(nameof(InvokeDoorOpen), RpcTarget.MasterClient);
			isOpen = true;
		}
	}

	[PunRPC]
	void OpenCoroutine()
	{
		StartCoroutine(RotateDoor());
	}

	void InvokeDoorOpen()
	{
		OnDoorOpen.Invoke();
	}

	IEnumerator RotateDoor()
	{
		while(door.transform.localRotation.eulerAngles.y < 89)
		{
			door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, Quaternion.Euler(0, 90, 0), 0.1f);
			yield return new WaitForSeconds(0.02f);
		}
	}

	private void OnDestroy()
	{
		OnDoorOpen = null;
	}
}
