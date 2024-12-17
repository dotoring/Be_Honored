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
	[SerializeField] BehaviorGraphAgent btAgent;

	BlackboardVariable<float> tem;

	public int moduleId;

	public PhotonView pv;
	public BehaviorGraphAgent behaviorAgent;
	public NavMeshAgent navMeshAgent;

	private void Awake()
	{
		btAgent = GetComponent<BehaviorGraphAgent>();
	}

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
						btAgent.BlackboardReference.GetVariable("DetectRange", out tem);
						tem.Value = App.Instance.warrior1.detectRange;
						btAgent.BlackboardReference.SetVariableValue("DetectRange", tem);
						btAgent.BlackboardReference.GetVariable("AttackRange", out tem);
						tem.Value = App.Instance.warrior1.attackRange;
						btAgent.BlackboardReference.SetVariableValue("AttackRange", tem);
						btAgent.BlackboardReference.GetVariable("AttackPower", out tem);
						tem.Value = App.Instance.warrior1.attackPower;
						btAgent.BlackboardReference.SetVariableValue("AttackPower", tem);
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
		btAgent.BlackboardReference.GetVariable("Hp", out tem);
		tem.Value -= damage;
		btAgent.BlackboardReference.SetVariableValue("Hp", tem);


	}

	[PunRPC]
	public void SetId(int id)
	{
		moduleId = id;
	}
}
