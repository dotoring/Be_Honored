using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MinoStateChange", story: "문이 열리면 [BossMonster] 를 이용해 [State] 를 변경하고 가장 [Distance] 가 가장 가까운 [Player] 를 추적합니다. 스킬 사용시 [BossSkills] 를 랜덤으로 정해줌 , [BossNavMeshAgent]", category: "Action", id: "9c27bfcfff5a8e01ea0436fb7b9649f7")]
public partial class MinoStateChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<BossState> State;
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<MinoSkill> BossSkills;
    [SerializeReference] public BlackboardVariable<UnityEngine.AI.NavMeshAgent> BossNavMeshAgent;
    protected override Status OnStart()
    {
        return Status.Running;
    }

	protected override Status OnUpdate()
	{

		if (BossMonster.Value.canUseSkill == true)
		{
			if (State.Value != BossState.Skill)
			{
				//BossSkills.Value = (MinoSkill)UnityEngine.Random.Range(0, 4);
				BossSkills.Value = global::MinoSkill.Stomp; //디버그용
				State.Value = BossState.Skill;
				BossNavMeshAgent.Value.ResetPath();
			}
		}
		else
		{
			Distance.Value = BossMonster.Value.detectRange;

			foreach (GameObject obj in BossMonster.Value.playerList)
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

