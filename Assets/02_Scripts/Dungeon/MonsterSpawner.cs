using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] GameObject monPref;

	private void Start()
	{
		foreach(Transform t in monSpawnPoints)
		{
			SpawnMonster(t.position);
		}
	}

	void SpawnMonster(Vector3 position)
	{
		Instantiate(monPref, position, Quaternion.identity);
	}
}
