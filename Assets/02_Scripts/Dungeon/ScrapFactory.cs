using Photon.Pun;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class ScrapFactory : MonoBehaviour
{
	public void SpawnScrap(Transform position)
	{
		int rand = Random.Range(0, 4);
		Vector3 pos = position.position;
		pos.y += 0.2f;
		switch (rand)
		{
			case 0:
				PhotonNetwork.InstantiateRoomObject("Glove", pos, Quaternion.identity);
				break;
			case 1:
				PhotonNetwork.InstantiateRoomObject("Helmet", pos, Quaternion.identity);
				break;
			case 2:
				PhotonNetwork.InstantiateRoomObject("Pants", pos, Quaternion.identity);
				break;
			case 3:
				PhotonNetwork.InstantiateRoomObject("Tunic", pos, Quaternion.identity);
				break;

		}
	}

	public void SpawnScraps(Transform[] positions, ModuleType type)
	{
		bool isBonus = Random.Range(0, 2) == 0;

		if (isBonus)
		{
			switch (type)
			{
				case ModuleType.Scraps:
					InstScrap(1, positions);
					break;
				case ModuleType.WeakMonsters:
					InstScrap(2, positions);
					break;
				case ModuleType.StrongMonsters:
					InstScrap(3, positions);
					break;
			}
		}
		else
		{
			switch (type)
			{
				case ModuleType.Scraps:
					InstScrap(0, positions);
					break;
				case ModuleType.WeakMonsters:
					InstScrap(1, positions);
					break;
				case ModuleType.StrongMonsters:
					InstScrap(2, positions);
					break;
			}
		}
	}

	//스크랩 생성 테이블
	void InstScrap(int i, Transform[] positions)
	{
		switch (i)
		{
			case 0:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				break;
			case 1:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[1].position, Quaternion.identity);
				break;
			case 2:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[1].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[2].position, Quaternion.identity);
				break;
			case 3:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[1].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_01", positions[2].position, Quaternion.identity);
				break;
			case 4:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[1].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[2].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_01", positions[3].position, Quaternion.identity);
				break;
			case 5:
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[0].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[1].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("GoldCup_02", positions[2].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_02", positions[3].position, Quaternion.identity);
				PhotonNetwork.InstantiateRoomObject("Candlestick_01", positions[4].position, Quaternion.identity);
				break;
		}
	}
}
