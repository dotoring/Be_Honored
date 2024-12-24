using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WavePatternStart", story: "[WaveObj] 를 활성화한다", category: "Action", id: "a1037100e6555b31cbf547b0b10adcda")]
public partial class WavePatternStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> WaveObj;
    protected override Status OnStart()
    {
		WaveObj.Value.SetActive(true);
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

