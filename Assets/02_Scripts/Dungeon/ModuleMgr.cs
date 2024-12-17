using System.Collections;
using UnityEngine;

public class ModuleMgr : MonoBehaviour
{
	public int moduleId;

	void Start()
	{
		DungeonMgr.instance?.AddModule(moduleId, gameObject);
	}
}
