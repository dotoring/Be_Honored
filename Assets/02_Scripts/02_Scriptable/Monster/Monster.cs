using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public enum MonsterType
{
	WARRIOR,
	ARCHER,

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
	public GameObject hpBar;

	private void Awake()
	{
		behaviorAgent = GetComponent<BehaviorGraphAgent>();
	}

	protected virtual void Start()
	{
		//if (!PhotonNetwork.IsMasterClient)
		//{
		//	behaviorAgent.enabled = false;
		//	navMeshAgent.enabled = false;
		//	hpBar.SetActive(false);
		//}
		////디버그 할때 주석
		//spawner = DungeonMgr.instance?.SetModule(moduleId).GetComponent<MonsterSpawner>();
		//spawner.AddToList(this.gameObject);

		//dieEvent += () => spawner.RemoveFromList(this.gameObject);

		//LoadData();
	}

	public void MonsterSetUP(MonsterType monsterTypePram, MonsterLevel monsterLevelPram)
	{
		typeOfMonster = monsterTypePram;
		monsterLevel = monsterLevelPram;
		LoadData();
	}


	private void LoadData()
	{
		switch (typeOfMonster)
		{
			case MonsterType.WARRIOR:
				switch (monsterLevel)
				{
					case MonsterLevel.A:
					case MonsterLevel.B:
					case MonsterLevel.C:
						detectRange = App.Instance.Warrior1.detectRange;
						attackRange = App.Instance.Warrior1.attackRange;
						attackPower = App.Instance.Warrior1.attackPower;
						hp = App.Instance.Warrior1.hp;
						break;
				}
				break;
			case MonsterType.ARCHER:
				switch (monsterLevel)
				{
					case MonsterLevel.A:
					case MonsterLevel.B:
					case MonsterLevel.C:
						detectRange = App.Instance.Archer1.detectRange;
						attackRange = App.Instance.Archer1.attackRange;
						attackPower = App.Instance.Archer1.attackPower;
						hp = App.Instance.Archer1.hp;
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
		if (hpBar != null)
			hpBar.SetActive(true);
		//hp -= damage;
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
