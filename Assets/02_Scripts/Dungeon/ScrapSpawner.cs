using Photon.Pun;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
	[SerializeField] Transform[] scrapSpawnPoints;

	private void Start()
	{
		SpawnScraps();
	}

	void SpawnScraps()
	{
		MainFactory.Inst.ModuleSpawn(ModuleType.Scraps, scrapSpawnPoints);
	}
}
