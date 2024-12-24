using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StonePatternStart", story: "[StoneObj]", category: "Action", id: "4df3d9f9f8edc156c0dbcc25c9d52f1d")]
public partial class StonePatternStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> StoneObj;

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

