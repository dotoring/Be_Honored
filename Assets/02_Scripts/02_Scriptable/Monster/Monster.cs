using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public enum MonsterType
{
	warrior,
}

public enum MonsterLevel
{
	A,
	B,
	C
}

public class Monster : MonoBehaviour
{
	public MonsterSpawner spawner;
	[SerializeField] MonsterType typeOfMonster;
	[SerializeField] MonsterLevel monsterLevel;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;
	public System.Action dieEvent;

	BlackboardVariable<float> tem;

	public int moduleId;

	public PhotonView pv;
	public BehaviorGraphAgent behaviorAgent;
	public NavMeshAgent navMeshAgent;

	private void Awake()
	{
		behaviorAgent = GetComponent<BehaviorGraphAgent>();
	}

	protected virtual void Start()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			behaviorAgent.enabled = false;
			navMeshAgent.enabled = false;
		}
		spawner = DungeonMgr.instance?.SetModule(moduleId).GetComponent<MonsterSpawner>();
		spawner.AddToList(this.gameObject);

		dieEvent += () => spawner.RemoveFromList(this.gameObject);

		LoadData();
	}

	private void LoadData()
	{
		switch (typeOfMonster)
		{
			case MonsterType.warrior:
				switch (monsterLevel)
				{
					case MonsterLevel.A:
					case MonsterLevel.B:
					case MonsterLevel.C:
						detectRange = App.Instance.warrior1.detectRange;
						attackRange = App.Instance.warrior1.attackRange;
						attackPower = App.Instance.warrior1.attackPower;
						hp			= App.Instance.warrior1.hp;
						break;
				}
				break;


			default:
				Debug.Log($" Error of {typeOfMonster}");
				break;
		}
	}

	public void Damaged(int damage)
	{
		hp -= damage;
		Debug.Log($" Monster {damage} Damaged remain {hp}");
		behaviorAgent.BlackboardReference.GetVariable("Hp", out tem);
		tem.Value -= damage;
		behaviorAgent.BlackboardReference.SetVariableValue("Hp", tem);


	}

	public void ActiveSelf()
	{
		Debug.Log("what");
		if (PhotonNetwork.IsMasterClient)
		{
			Debug.Log("the");
			behaviorAgent.enabled = true;
		}
	}


	[PunRPC]
	public void SetId(int id)
	{
		moduleId = id;
	}
}
