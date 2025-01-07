using Photon.Pun;
using System;
using UnityEngine;

public class PlayerEnterChecker : MonoBehaviour
{
	[SerializeField] GameObject shroud;
	

	public Action OnPlayerEnter = () => { };

	void PlayerEnter()
	{
		OnPlayerEnter.Invoke();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (other.GetComponent<PhotonView>().IsMine)
			{
				shroud.layer = LayerMask.NameToLayer("Default");
			}

			if(PhotonNetwork.IsMasterClient)
			{
				PlayerEnter();
			}
		}
	}
}
