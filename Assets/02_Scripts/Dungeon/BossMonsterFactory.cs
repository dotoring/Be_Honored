using Photon.Pun;
using UnityEngine;

public class BossMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 2);
		GameObject go;

		switch (rand)
		{
			case 0:
				go = PhotonNetwork.InstantiateRoomObject("Cerberus", positions[0].position, Quaternion.identity);
				go.GetComponent<BossMonster>().resetPos = positions[0];
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 1:
				//미노타우르스 완성되면 바꿀 것
				go = PhotonNetwork.InstantiateRoomObject("Cerberus", positions[0].position, Quaternion.identity);
				go.GetComponent<BossMonster>().resetPos = positions[0];
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
		}
	}
}
