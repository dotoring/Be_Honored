using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Photon.Pun;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartIdleAni", story: "[BossMonster]", category: "Action", id: "7a1852d945c8a01a033a9ea91f10b295")]
public partial class StartIdleAniAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("idle");
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

