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
		Debug.Log("스크랩 스폰");
		MainFactory.Inst.ModuleReward(moduleMgr.moduleType, monSpawnPoints);
	}

	public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
	{
		//문 열린 모듈만 몬스터 행동 시작
		if(moduleMgr.isOpen)
		{
			foreach (GameObject monster in spawnedMonsters)
			{
				monster.GetComponent<Monster>().ActiveSelf();
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
