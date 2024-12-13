using System.Collections.Generic;
using UnityEngine;

public class MonsterFactory : MonoBehaviour
{
	public List<GameObject> monsters = new List<GameObject>();

	public void SpawnMonster(int type, Vector3 position)
	{
		if (type < monsters.Count)
		{
			Instantiate(monsters[0], position, Quaternion.identity);
		}
		else
		{
			Debug.LogWarning("Wrong number of monster type");
		}
	}
}
