using System;
using UnityEngine;

public class RespawnChecker : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			DungeonMgr.instance.RemovePlayer(other.gameObject);
		}
	}
}
