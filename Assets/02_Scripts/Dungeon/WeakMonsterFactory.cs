using Photon.Pun;
using UnityEngine;

public class WeakMonsterFactory : MonoBehaviour
{
	public void SpawnMonster(Transform[] positions, int moduleId)
	{
		int rand = Random.Range(0, 2);

		switch (rand)
		{
			case 0:
				for (int i = 0; i < 2; i++)
				{
					//몬스터 디버그용
					//GameObject go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[i].position, Quaternion.identity);
					GameObject go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				break;
			case 1:
				for (int i = 0; i < 3; i++)
				{
					//몬스터 디버그용
					//GameObject go = PhotonNetwork.InstantiateRoomObject("Skeleton_Sorcerer", positions[i].position, Quaternion.identity);
					GameObject go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", positions[i].position, Quaternion.identity);
					go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, moduleId);
				}
				break;
		}
	}
}
