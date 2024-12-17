using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CastingTimeLeft", story: "[CastingTime] 이 지나간다", category: "Action", id: "27be6883cae091045a21ad88e995d600")]
public partial class CastingTimeLeftAction : Action
{
    [SerializeReference] public BlackboardVariable<float> CastingTime;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
		CastingTime.Value += Time.deltaTime;
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

