using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Behavior;

public class MonsterSpawner : MonoBehaviourPunCallbacks
{
	[SerializeField] ModuleMgr moduleMgr;
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] List<GameObject> spawnedMonsters;
	MainFactory mainFactory;

	private void Start()
	{
		moduleMgr = GetComponent<ModuleMgr>();
		if(PhotonNetwork.IsMasterClient)
		{
			mainFactory.ModuleInfo(moduleMgr.moduleType, monSpawnPoints, moduleMgr.moduleId);
		}

		moduleMgr.OnRoomOpen += BeginMonsterBT;
	}

	private void OnDestroy()
	{
		moduleMgr.OnRoomOpen -= BeginMonsterBT;
	}

	public void SetFactory(MainFactory _mainFactory)
	{
		mainFactory = _mainFactory;
	}

	public void AddToList(GameObject monster)
	{
		spawnedMonsters.Add(monster);
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
		//Instantiate(scrapFactory.GetPref(0), monSpawnPoints[0].position, Quaternion.identity);
		mainFactory.ModuleInfo(ModuleType.Scraps, monSpawnPoints, moduleMgr.moduleId);
	}

	public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
	{
		if (PhotonNetwork.IsMasterClient)
		{
			foreach (GameObject monster in spawnedMonsters)
			{
				monster.GetComponent<Monster>().behaviorAgent.enabled = true;
				monster.GetComponent<Monster>().navMeshAgent.enabled = true;
			}
		}
	}

	void BeginMonsterBT()
	{
		foreach(GameObject monster in spawnedMonsters)
		{
			monster.GetComponent<Monster>().ActiveSelf();
		}
	}
}
