using Photon.Pun;
using UnityEngine;

public class Magma : MonoBehaviour
{
	[SerializeField]private float damageTimer;

	private void Start()
	{
		Destroy(this.gameObject, 10.0f);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			print("player 있음");
			damageTimer += Time.deltaTime;

			if (damageTimer >= 1f)
			{
				if (PhotonNetwork.IsMasterClient)
				{
					other.GetComponent<HitPlayer>()?.Damaged(1);
				}
				damageTimer = 0f;
			}
		}
	}
}
