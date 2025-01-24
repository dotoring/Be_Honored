using Photon.Pun;
using UnityEngine;

public class SorcererFireBall : MonoBehaviour
{
	float speed;
	float damage;
	Vector3 dis;

	float radius=3.0f;
	public void InitData(Vector3 distance, float speeds, float damaged)
	{
		speed=speeds;
		damage=damaged;
		dis = distance;
	}

	private void Start()
	{
		Destroy(gameObject, 3.5f);
	}

	private void OnDestroy()
	{
		Explosion();
	}

	private void Update()
	{
		transform.position += Time.deltaTime * speed * dis;
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (PhotonNetwork.IsMasterClient)
			{
				other.GetComponent<HitPlayer>()?.Damaged(1f*damage);
			}
			PhotonNetwork.Destroy(gameObject);
		}
	}

	private void Explosion()
	{
		Collider[] col = Physics.OverlapSphere(transform.position, radius,10);
		if(PhotonNetwork.IsMasterClient)
		{
			foreach(Collider c in col)
			{
				c.GetComponent<HitPlayer>()?.Damaged(0.5f*damage);
			}
		}
	}
}
