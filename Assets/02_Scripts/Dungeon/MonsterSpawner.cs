using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Behavior;

public class MonsterSpawner : MonoBehaviourPunCallbacks
{
	[SerializeField] ModuleMgr moduleMgr;
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] List<GameObject> spawnedMonsters;

	private void Start()
	{
		moduleMgr = GetComponent<ModuleMgr>();
		if(PhotonNetwork.IsMasterClient)
		{
			MainFactory.Inst.ModuleSpawn(moduleMgr.moduleType, monSpawnPoints, moduleMgr.moduleId);
		}

		moduleMgr.OnRoomOpen += BeginMonsterBT;
	}

	private void OnDestroy()
	{
		moduleMgr.OnRoomOpen -= BeginMonsterBT;
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
		MainFactory.Inst.ModuleReward(moduleMgr.moduleType, monSpawnPoints);
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
