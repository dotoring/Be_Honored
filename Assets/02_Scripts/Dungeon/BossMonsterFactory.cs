using Photon.Pun;
using UnityEngine;

public class BossMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 2);
		GameObject go = null;

		switch (rand)
		{
			case 0:
				go = PhotonNetwork.InstantiateRoomObject("Cerberus", positions[0].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 1:
				go = PhotonNetwork.InstantiateRoomObject("Cerberus", positions[0].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
		}
	}
}
