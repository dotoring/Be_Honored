using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] MonsterFactory monsterFactory;

	private void Start()
	{
		monsterFactory.SpawnMonster(0, monSpawnPoints[0].position);
	}

	public void SetFactory(MonsterFactory factory)
	{
		monsterFactory = factory;
	}
}
