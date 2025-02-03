using Photon.Pun;
using UnityEngine;

public class StrongMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 6);
		GameObject go;
		switch (rand)
		{
			case 0:
				//워1 아1 소1
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[0].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[1].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[2].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 1:
				//워2 아1 소1
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[2].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[3].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 2:
				//워2 아2
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[i+2].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				break;
			case 3:
				//워2 소2
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[i+2].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				break;
			case 4:
				//워2 아2 소1
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				for (int i = 0; i < 2; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[i+2].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[4].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
			case 5:
				//워3 아1 소1
				for (int i = 0; i < 3; i++)
				{
					go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_archier", positions[3].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[4].position, Quaternion.identity);
				go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				break;
		}
	}
}
