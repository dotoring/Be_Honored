using System.Collections;
using UnityEngine;

public class ModuleMgr : MonoBehaviour
{
	public int moduleId;
	public ModuleType moduleType;

	void Start()
	{
		DungeonMgr.instance?.AddModule(moduleId, gameObject);
	}
}
