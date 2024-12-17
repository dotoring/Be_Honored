using System.Collections.Generic;
using UnityEngine;

public class DungeonMgr : MonoBehaviour
{
	public static DungeonMgr instance;

    public Dictionary<int, GameObject> modules = new Dictionary<int, GameObject>();

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void AddModule(int id, GameObject module)
	{
		Debug.Log($"{id} Module {module.name}");
		modules.Add(id, module);
	}

	public GameObject SetModule(int id)
	{
		return modules[id];
	}
}
