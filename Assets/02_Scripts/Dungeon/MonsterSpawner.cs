using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Behavior;

public class MonsterSpawner : MonoBehaviourPunCallbacks
{
	int id;
	[SerializeField] bool isBoss;
	[SerializeField] ModuleMgr moduleMgr;
	[SerializeField] Transform[] monSpawnPoints;
	[SerializeField] List<GameObject> spawnedMonsters;
	MonsterFactory monsterFactory;
	ScrapFactory scrapFactory;

	private void Start()
	{
		moduleMgr = GetComponent<ModuleMgr>();
		id = moduleMgr.moduleId;
		if(PhotonNetwork.IsMasterClient)
		{
			if(isBoss)
			{
				monsterFactory.SpawnObejct("Cerberus", monSpawnPoints[0].position, id);
			}
			else
			{
				int monsterCount = Random.Range(1, 4);
				for (int i = 0; i < monsterCount; i++)
				{
					//GameObject go = PhotonNetwork.InstantiateRoomObject("Skeleton_warrior", monSpawnPoints[i].position, Quaternion.identity);
					//go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, id);

					monsterFactory.SpawnObejct("Skeleton_warrior", monSpawnPoints[i].position, id);
				}
			}
		}
	}

	public void SetFactory(MonsterFactory _monsterFactory, ScrapFactory _scrapFactory)
	{
		monsterFactory = _monsterFactory;
		scrapFactory = _scrapFactory;
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
		Instantiate(scrapFactory.GetPref(0), monSpawnPoints[0].position, Quaternion.identity);
	}

	public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
	{
		Debug.Log("1");
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log("2");
			foreach (GameObject monster in spawnedMonsters)
			{
				monster.GetComponent<Monster>().behaviorAgent.enabled = true;
				monster.GetComponent<Monster>().navMeshAgent.enabled = true;
			}
		}
	}
}
