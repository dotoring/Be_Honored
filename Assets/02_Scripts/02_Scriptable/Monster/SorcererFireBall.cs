using Photon.Pun;
using UnityEngine;

public class SorcererFireBall : MonoBehaviour
{
	float speed;
	int damage;
	Vector3 dis;
	[SerializeField] bool isPlayers;

	float radius=3.0f;
	public void InitData(Vector3 distance, float speeds, float damaged)
	{
		speed=speeds;
		damage=(int)damaged;
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
		if (isPlayers)
		{
			if (other.CompareTag("Monster"))
			{
				if (PhotonNetwork.IsMasterClient)
				{
					other.GetComponent<PhotonView>().RPC("Damaged",
						RpcTarget.AllBuffered, 10*damage);
				}
				PhotonNetwork.Destroy(gameObject);
			}
		}
		else
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
	}

	private void Explosion()
	{
		if (isPlayers)
		{
			Collider[] col = Physics.OverlapSphere(transform.position, radius,11);
			if(PhotonNetwork.IsMasterClient)
			{
				foreach(Collider c in col)
				{
					Debug.Log(c.gameObject.name);
					c.GetComponent<PhotonView>().RPC("Damaged", RpcTarget.AllBuffered, 5*damage);
				}
			}
		}
		else
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
}
