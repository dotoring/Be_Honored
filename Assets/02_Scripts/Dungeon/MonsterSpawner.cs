using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] List<GameObject> spawnedMonsters;
	MonsterFactory monsterFactory;
	ScrapFactory scrapFactory;

	private void Start()
	{
		int monsterCount = Random.Range(1, 4);
		for (int i = 0; i < monsterCount; i++)
		{
			GameObject go = Instantiate(monsterFactory.GetPref(0), monSpawnPoints[i].position, Quaternion.identity);
			go.GetComponent<Monster>().spawner = this;
			go.GetComponent<Monster>().hp = 10;
			spawnedMonsters.Add(go);
		}
	}

	public void SetFactory(MonsterFactory _monsterFactory, ScrapFactory _scrapFactory)
	{
		monsterFactory = _monsterFactory;
		scrapFactory = _scrapFactory;
	}

	public void RemoveFromList(GameObject monster)
	{
		try
		{
			spawnedMonsters.Remove(monster);
		}
		catch
		{
			Debug.LogWarning($"{monster.name} is not in monster list");
		}
		CheckMonsters();
	}

	void CheckMonsters()
	{
		if (spawnedMonsters.Count <= 0)
		{
			SpawnScraps();
		}
	}

	void SpawnScraps()
	{
		Instantiate(scrapFactory.GetPref(0), monSpawnPoints[0].position, Quaternion.identity);
	}
}
