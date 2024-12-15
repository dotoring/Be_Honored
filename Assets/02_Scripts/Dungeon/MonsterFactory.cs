using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterFactory : Factory
{
	public List<GameObject> monsters = new List<GameObject>();

	public override GameObject GetPref(int type)
	{
		try
		{
			return monsters[type];
		}
		catch
		{
			Debug.LogWarning("Wrong number of monster type");
			return null;
		}
	}
}
