using Photon.Pun;
using UnityEngine;

public class StrongMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 2);
		GameObject go;
		switch (rand)
		{
			case 0:
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[2].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 1:
				for (int i = 0; i < 3; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[3].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
		}
	}
}
