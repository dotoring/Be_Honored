using System;
using Photon.Pun;
using UnityEngine;

public class LightningShock : MonoBehaviour
{
	[SerializeField] private Collider collider;

	private void Start()
	{
		Invoke(nameof(ColliderOff), 1f);
		Invoke(nameof(Destroy), 2.5f);
	}

	void ColliderOff()
	{
		collider.enabled = false;
	}

	void Destroy()
	{
		PhotonNetwork.Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Monster"))
		{
			if (PhotonNetwork.IsMasterClient)
			{
				other.GetComponent<PhotonView>().RPC("Damaged",
					RpcTarget.AllBuffered, 3*Player.Instance._stat.attack);
			}
		}
	}
}
