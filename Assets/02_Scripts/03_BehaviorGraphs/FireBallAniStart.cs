using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FireBallAniStart", story: "[BossMonster]", category: "Action", id: "391901d0b85aa1afdc24f75f463439ee")]
public partial class FireBallAniStart : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("FireBallPattern");
		BossMonster.Value.StartPatternRPC(2);
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

