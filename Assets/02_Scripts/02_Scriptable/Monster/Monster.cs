using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;


public enum MonsterType
{
	WARRIOR,
	ARCHER,
	SORCERCER,
	FOOTMAN,
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

public class Monster : MonoBehaviourPunCallbacks
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
	public bool isDie;

	BlackboardVariable<float> tem = new ();
	//[SerializeField]BlackboardVariable<GameObject> targettem = new ();
	public int moduleId;

	public Collider col;
	public PhotonView pv;
	public BehaviorGraphAgent behaviorAgent;
	public NavMeshAgent navMeshAgent;
	public Canvas hpBar;
	public Animator ani;

	[SerializeField] private Transform shootPoint;

	public GameObject Ball;

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
			if(hpBar!=null)
				hpBar.gameObject.SetActive(false);
		}
		spawner = DungeonMgr.instance?.SetModule(moduleId).GetComponent<MonsterSpawner>();
		spawner.AddToList(this.gameObject);
		dieEvent += () => spawner.RemoveFromList(this.gameObject);
		dieEvent += () => MainFactory.Inst.MonsterDrop(transform);
		dieEvent += () => isDie = true;
		dieEvent += () => col.enabled = false;
		if(typeOfMonster!=MonsterType.BOSS)
			dieEvent += () => pv.RPC(nameof(DieRPC), RpcTarget.All);
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
			case MonsterType.SORCERCER:
				switch (monsterLevel)
				{
					case MonsterLevel.A:
					case MonsterLevel.B:
					case MonsterLevel.C:
						detectRange = App.Instance.Sorcerer1.detectRange;
						attackRange = App.Instance.Sorcerer1.attackRange;
						attackPower = App.Instance.Sorcerer1.attackPower;
						maxHp		= App.Instance.Sorcerer1.hp;
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
		if (curHp > 0)
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
			if (curHp <= 0 && isDie == false)
			{
				dieEvent.Invoke();
			}
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

	[PunRPC]
	public void AttackAniRPC(bool isAttack)
	{
		ani.SetBool("Attack",isAttack);
	}


	public void Attack()
	{
		behaviorAgent.BlackboardReference.GetVariableValue("Player", out Transform ob);
		Vector3 temp = ob.transform.position - transform.position;
		temp.y = 0;
		transform.forward = temp.normalized;
		if (typeOfMonster == MonsterType.SORCERCER)
		{
			Vector3 ballDis =(ob.position - shootPoint.position);
			ballDis.y = 0;
			ballDis=ballDis.normalized;
			GameObject ball = PhotonNetwork.Instantiate("SocererFireBall", shootPoint.position, Quaternion.identity);
			ball.GetComponent<SorcererFireBall>().InitData(ballDis,3.0f, attackPower);
		}
		else
		{
			if (behaviorAgent.enabled == true)
			{
				ob.GetComponent<HitPlayer>()?.Damaged(attackPower);
			}
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.forward*10+transform.position);
	}
}
