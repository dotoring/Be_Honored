using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Collections;

public class Monster : MonoBehaviour
{
	public MonsterSpawner spawner;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;

	public int moduleId;

	public System.Action dieEvent;

	public PhotonView pv;
	public BehaviorGraphAgent behaviorAgent;
	public NavMeshAgent navMeshAgent;

	private void Start()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			behaviorAgent.enabled = false;
			navMeshAgent.enabled = false;
		}
		spawner = DungeonMgr.instance?.SetModule(moduleId).GetComponent<MonsterSpawner>();
		spawner.AddToList(this.gameObject);

		dieEvent += () => spawner.RemoveFromList(this.gameObject);
	}

	[PunRPC]
	public void SetId(int id)
	{
		moduleId = id;
	}
}
