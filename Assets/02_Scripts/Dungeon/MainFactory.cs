using System.Numerics;
using Photon.Pun;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MainFactory : MonoBehaviour
{
	public static MainFactory Inst;
	[SerializeField] WeakMonsterFactory wmf;
	[SerializeField] StrongMonsterFactory smf;
	[SerializeField] BossMonsterFactory bmf;
	[SerializeField] ScrapFactory sf;

	private void Awake()
	{
		if (Inst != null)
		{
			Destroy(gameObject);
		}
		Inst = this;
	}

	public void ModuleSpawn(ModuleType moduleType, Transform[] positions, int moduleId = 0)
	{
		switch (moduleType)
		{
			case ModuleType.WeakMonsters:
				wmf.SpawnMonster(positions, moduleId);
				break;
			case ModuleType.StrongMonsters:
				smf.SpawnMonster(positions, moduleId);
				break;
			case ModuleType.Scraps:
				sf.SpawnScraps(positions, moduleType);
				break;
			case ModuleType.Boss:
				bmf.SpawnMonster(positions, moduleId);
				break;
		}
	}

	public void ModuleReward(ModuleType moduleType, Transform[] positions)
	{
		if(PhotonNetwork.IsMasterClient)
		{
			sf.SpawnScraps(positions, moduleType);
		}
	}

	public void MonsterDrop(Transform pos)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			//확률 드랍
			int rand = Random.Range(0, 10);
			if (rand < 4)
			{
				sf.SpawnScrap(pos);
			}
		}
	}
}
