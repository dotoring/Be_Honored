using Photon.Pun;
using UnityEngine;

public class WeakMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 4);
		GameObject go = null;

		switch (rand)
		{
			case 0:
				//워리어 2
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				break;
			case 1:
				//워리어1 아쳐1
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[0].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[1].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 2:
				//워리어1 소서러1
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[0].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[1].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 3:
				//워리어2 아처1
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[2].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
		}
	}
}
