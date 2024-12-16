using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting.FullSerializer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StateChange", story: "문이 열리면 [BossMonster] 를 이용해 [State] 를 변경하고 가장 [Distance] 가 가장 가까운 [Player] 를 추적합니다", category: "Action", id: "6a07177c187212baf861e0bd3746a4f0")]
public partial class StateChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<BossState> State;
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<bool> IsDoorOpen;


    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
		if (IsDoorOpen.Value == false)
		{ 
			IsDoorOpen.Value = BossMonster.Value.isDoorOpen;
		}
		else if (IsDoorOpen.Value == true)
		{
			Debug.Log("dd");
			if (BossMonster.Value.canUseSkill == true)
				State.Value = BossState.Skill;
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
					}
					State.Value = BossState.Move;
				}

				if (Distance.Value < BossMonster.Value.attackRange)
					State.Value = BossState.Attack;
			}
		}
		
        return Status.Success;
    }

}

