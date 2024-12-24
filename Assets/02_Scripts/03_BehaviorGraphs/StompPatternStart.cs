using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StompPatternStart", story: "[StompObj]", category: "Action", id: "6b7d63761fde57463c03dccec8749da1")]
public partial class StompPatternStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> StompObj;
    protected override Status OnStart()
    {
		StompObj.Value.SetActive(true);
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

