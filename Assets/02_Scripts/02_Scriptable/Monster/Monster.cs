using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using UnityEditor.Animations;


public enum MonsterType
{
	WARRIOR,
	ARCHER,

	BOSS,
}
public enum BossType
{
	NOTBOSS,
	CERBERUS,
	MINOTAUR
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
	[SerializeField] BossType bossType;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float maxHp;
	public float curHp;
	public System.Action dieEvent;

	[SerializeField]BlackboardVariable<float> tem=new BlackboardVariable<float>();

	public int moduleId;

	public PhotonView pv;
	public BehaviorGraphAgent behaviorAgent;
	public NavMeshAgent navMeshAgent;
	public Canvas hpBar;
	public Animator ani;

	private void Awake()
	{
		behaviorAgent = GetComponent<BehaviorGraphAgent>();
	}

	protected virtual void Start()
	{
		//디버그 할때 주석
		if (!PhotonNetwork.IsMasterClient)
		{
			behaviorAgent.enabled = false;
			navMeshAgent.enabled = false;
			if(hpBar!=null)
				hpBar.gameObject.SetActive(false);
		}
		spawner = DungeonMgr.instance?.SetModule(moduleId).GetComponent<MonsterSpawner>();
		spawner.AddToList(this.gameObject);
		dieEvent += () => spawner.RemoveFromList(this.gameObject);
		dieEvent += () => MainFactory.Inst.MonsterDrop(transform);
		dieEvent += () => pv.RPC(nameof(DieRPC), RpcTarget.AllBuffered);
		LoadData();
	}

	[PunRPC]
	public void DieRPC()
	{
		ani.SetTrigger("Die");
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
						detectRange		= App.Instance.Warrior1.detectRange;
						attackRange		= App.Instance.Warrior1.attackRange;
						attackPower		= App.Instance.Warrior1.attackPower;
						maxHp			= App.Instance.Warrior1.hp;
						curHp			= maxHp;
						break;
				}
				break;
			case MonsterType.ARCHER:
				switch (monsterLevel)
				{
					case MonsterLevel.A:
					case MonsterLevel.B:
					case MonsterLevel.C:
						detectRange		= App.Instance.Archer1.detectRange;
						attackRange		= App.Instance.Archer1.attackRange;
						attackPower		= App.Instance.Archer1.attackPower;
						maxHp			= App.Instance.Archer1.hp;
						curHp = maxHp;
						break;
				}
				break;
			case MonsterType.BOSS:
				switch (bossType)
				{
					case BossType.CERBERUS:
						switch (monsterLevel)
						{
							case MonsterLevel.A:
							case MonsterLevel.B:
							case MonsterLevel.C:
								detectRange		= App.Instance.Cerbe1.detectRange;
								attackRange		= App.Instance.Cerbe1.attackRange;
								attackPower		= App.Instance.Cerbe1.attackPower;
								maxHp			= App.Instance.Cerbe1.hp;
								curHp = maxHp;
								break;
						}
						break;
					case BossType.MINOTAUR:
						switch (monsterLevel)
						{
							case MonsterLevel.A:
							case MonsterLevel.B:
							case MonsterLevel.C:
								detectRange		= App.Instance.Mino1.detectRange;
								attackRange		= App.Instance.Mino1.attackRange;
								attackPower		= App.Instance.Mino1.attackPower;
								maxHp			= App.Instance.Mino1.hp;
								curHp = maxHp;
								break;
						}
						break;
				}
				break;

			default:
				Debug.Log($" Error of {typeOfMonster}");
				break;
		}
	}

	[PunRPC]
	public void Damaged(int damage)
	{
		Debug.Log($" {gameObject.name} {damage} Damaged remain {curHp}");
		curHp -= damage;
		tem.Value = curHp;
		behaviorAgent.BlackboardReference.SetVariableValue<float>("Hp", tem);
		if (hpBar != null)
		{
			hpBar.gameObject.SetActive(true);
			float hpPer = curHp / maxHp;
			hpBar.GetComponent<LookCamera>().UpdateUI(hpPer);
		}
	}

	public void ActiveSelf()
	{
		if (PhotonNetwork.IsMasterClient)
		{
			behaviorAgent.enabled = true;
		}
	}


	[PunRPC]
	public void SetId(int id)
	{
		moduleId = id;
	}
}
