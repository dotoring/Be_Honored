using UnityEngine;

public class MainFactory : MonoBehaviour
{
	[SerializeField] WeakMonsterFactory wmf;
	[SerializeField] BossMonsterFactory bmf;
	[SerializeField] ScrapFactory sf;

    public void ModuleInfo(ModuleType moduleType, Transform[] positions, int moduleId)
	{
		switch (moduleType)
		{
			case ModuleType.Monsters:
				wmf.SpawnMonster(positions, moduleId);
				break;
			case ModuleType.Scraps:
				sf.SpawnScrap(positions[0]);
				break;
			case ModuleType.Boss:
				bmf.SpawnMonster(positions, moduleId);
				break;
		}
	}
}
