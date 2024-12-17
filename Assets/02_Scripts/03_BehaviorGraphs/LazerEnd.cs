using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LazerEnd", story: "[BossMonster] 에게 스킬이 끝났음을 보냄", category: "Action", id: "5fe2d72d1393d140275c8fa60fcfe77f")]
public partial class LazerEnd : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;

    protected override Status OnStart()
    {
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

