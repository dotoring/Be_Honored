using UnityEngine;

public class MainFactory : MonoBehaviour
{
	[SerializeField] WeakMonsterFactory wmf;
	[SerializeField] StrongMonsterFactory smf;
	[SerializeField] BossMonsterFactory bmf;
	[SerializeField] ScrapFactory sf;

    public void ModuleSpawn(ModuleType moduleType, Transform[] positions, int moduleId)
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
		sf.SpawnScraps(positions, moduleType);
	}
}
