using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HowlingAniStart", story: "[BossMonster]", category: "Action", id: "f4bd8edb1dbe2e6c564cfcec675f4c9b")]
public partial class HowlingAniStart : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("HowlingPattern");
		BossMonster.Value.StartPatternRPC(3);
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

