using NUnit.Framework;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterChecker : MonoBehaviour
{
	[SerializeField] GameObject shroud;


	public Action OnPlayerEnter = () => { };

	public List<GameObject> playerList;

	void PlayerEnter()
	{
		Debug.Log("PlayerEnter");
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

			DungeonMgr.instance.AddPlayer(other.gameObject);
		}
	}
}
