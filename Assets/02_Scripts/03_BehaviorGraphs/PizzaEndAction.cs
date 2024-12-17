using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaEnd", story: "스킬 범위 안에 있는 Player 들에게 [Damage] 를 주고 스킬이 끝났음을 [BossMonster] 에게 전해준다", category: "Action", id: "89e7bac8be838bbcbcc0296c4dbf1a07")]
public partial class PizzaEndAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Damage;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;

    protected override Status OnStart()
    {
		BossMonster.Value.ResetSkill();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

