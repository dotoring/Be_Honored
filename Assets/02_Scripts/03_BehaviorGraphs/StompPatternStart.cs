using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StompPatternStart", story: "[StompObj] 활성화 [BossMonsert] 를 넘겨줌", category: "Action", id: "6b7d63761fde57463c03dccec8749da1")]
public partial class StompPatternStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> StompObj;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonsert;

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

