using UnityEngine;
using Photon.Pun;

public class HitPlayer : MonoBehaviour
{
	PhotonView pv;

	private void Awake()
	{
		pv = GetComponent<PhotonView>();
	}

	public void Damaged(float damage)
	{
		//if(pv.IsMine)
		//{
		//	Player.Instance.Damaged(damage);
		//}
		//else
		//{
		//	pv.RPC(nameof(DamageRpc), PhotonNetwork.CurrentRoom.GetPlayer(pv.Owner.ActorNumber), damage);
		//}
		pv.RPC(nameof(DamageRpc), PhotonNetwork.CurrentRoom.GetPlayer(pv.Owner.ActorNumber), damage);
	}

	[PunRPC]
	void DamageRpc(float damage)
	{
		Player.Instance.Damaged(damage);
	}
}
