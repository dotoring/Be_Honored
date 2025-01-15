using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMgr : MonoBehaviour
{
	public static DungeonMgr instance;

	public PhotonView pv;
    public Dictionary<int, GameObject> modules = new Dictionary<int, GameObject>();

	public List<GameObject> playerListInBoss = new List<GameObject>();

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

		pv = GetComponent<PhotonView>();
	}

	public void AddModule(int id, GameObject module)
	{
		modules.Add(id, module);
	}

	public GameObject SetModule(int id)
	{
		return modules[id];
	}

	public void AddPlayer(GameObject go)
	{
		if (!playerListInBoss.Contains(go))
		{
			playerListInBoss.Add(go);
		}
	}
}
