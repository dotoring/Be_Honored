using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LazerAniStart", story: "[BossMonster]", category: "Action", id: "4168b5d863880ad1b6b2266100828fe0")]
public partial class LazerAniStart : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("LazerPattern");
		BossMonster.Value.StartPatternRPC(1);
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

