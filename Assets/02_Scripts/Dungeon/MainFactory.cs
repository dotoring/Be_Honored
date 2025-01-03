using Photon.Pun;
using UnityEngine;

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
			sf.SpawnScrap(pos);
		}
	}
}
