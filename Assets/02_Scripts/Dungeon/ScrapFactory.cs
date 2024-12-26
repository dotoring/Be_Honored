using Photon.Pun;
using UnityEngine;

public class ScrapFactory : MonoBehaviour
{
	public void SpawnScrap(Transform position)
	{
		int rand = Random.Range(0, 3);

		switch (rand)
		{
			case 0:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", position.position, Quaternion.identity);
				break;
			case 1:
				PhotonNetwork.InstantiateRoomObject("Candlestick_01", position.position, Quaternion.identity);
				break;
			case 2:
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", position.position, Quaternion.identity);
				break;
		}
	}

	public void SpawnScraps(Transform[] positions)
	{
		for (int i = 0; i < positions.Length; i++)
		{
			int rand = Random.Range(0, 3);

			switch (rand)
			{
				case 0:
					PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[i].position, Quaternion.identity);
					break;
				case 1:
					PhotonNetwork.InstantiateRoomObject("Candlestick_01", positions[i].position, Quaternion.identity);
					break;
				case 2:
					PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[i].position, Quaternion.identity);
					break;
			}
		}
	}
}
