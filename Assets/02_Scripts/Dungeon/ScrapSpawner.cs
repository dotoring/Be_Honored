using Photon.Pun;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
	ModuleMgr moduleMgr;
	MainFactory mainFactory;
	[SerializeField] Transform[] scrapSpawnPoints;

	private void Start()
	{
		moduleMgr = GetComponent<ModuleMgr>();
		SpawnScraps();
	}

	public void SetFactory(MainFactory _mainFactory)
	{
		mainFactory = _mainFactory;
	}

	void SpawnScraps()
	{
		mainFactory.ModuleSpawn(ModuleType.Scraps, scrapSpawnPoints, moduleMgr.moduleId);
	}
}
