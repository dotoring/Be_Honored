using Photon.Pun;
using UnityEngine;

public class ScrapFactory : MonoBehaviour
{
	public void SpawnScrap(Transform[] positions)
	{
		int rand = Random.Range(0, 2);

		switch (rand)
		{
			case 0:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				break;
			case 1:
				PhotonNetwork.InstantiateRoomObject("Candlestick_01", positions[0].position, Quaternion.identity);
				break;
			case 2:
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[0].position, Quaternion.identity);
				break;
		}
	}
}
