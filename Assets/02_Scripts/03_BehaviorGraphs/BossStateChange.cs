using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossStateChange", story: "문이 열리면 [BossMonster] 를 이용해 [State] 를 변경하고 가장 [Distance] 가 가장 가까운 [Player] 를 추적합니다. 스킬 사용시 [BossSkills] 를 랜덤으로 정해줌 , [BossNavMeshAgent] [resetpos]", category: "Action", id: "6a07177c187212baf861e0bd3746a4f0")]
public partial class StateChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<BossState> State;
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<CerberusSkills> BossSkills;
    [SerializeReference] public BlackboardVariable<NavMeshAgent> BossNavMeshAgent;
    [SerializeReference] public BlackboardVariable<Transform> Resetpos;
    [SerializeReference] public BlackboardVariable<bool> IsDoorOpen;


	protected override Status OnStart()
	{
		return Status.Running;
	}

	protected override Status OnUpdate()
	{
		if (BossMonster.Value.playerList.Count == 0)
		{
			Resetpos.Value = BossMonster.Value.resetPos;
			State.Value = BossState.Idle;
			return Status.Success;
		}
		if (BossMonster.Value.canUseSkill == true)
		{
			if (State.Value != BossState.Skill)
			{
				BossSkills.Value = (CerberusSkills)UnityEngine.Random.Range(0, 4);
				//BossSkills.Value = global::CerberusSkills.Pizza; //디버그용
				State.Value = BossState.Skill;
				BossNavMeshAgent.Value.ResetPath();
			}
		}
		else
		{
			Distance.Value = BossMonster.Value.detectRange;

			foreach (GameObject obj in BossMonster.Value.playerList)//DungeonMgr.instance.playerListInBoss)
			{
				float dis2 = Vector3.Distance(BossMonster.Value.transform.position, obj.transform.position);
				if (Distance.Value > dis2)
				{
					Distance.Value = dis2;
					Player.Value = obj;
					BossMonster.Value.targetPlayer = obj;
				}
				State.Value = BossState.Move;
			}

			if (Distance.Value < BossMonster.Value.attackRange)
				State.Value = BossState.Attack;

		}
			return Status.Success;
	}

}


