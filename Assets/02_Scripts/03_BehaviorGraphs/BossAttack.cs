using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossAttack", story: "[BossMonster]", category: "Action", id: "9ba506786b29c946d202813922f5d0c2")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("attack");
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

