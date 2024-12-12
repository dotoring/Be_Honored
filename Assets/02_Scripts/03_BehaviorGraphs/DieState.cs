using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DieState", story: "[hp] 의 수치를 보고 [state] 를 변경", category: "Action", id: "bcee7e2211620c910592ccb60a439978")]
public partial class DieStateAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Hp;
    [SerializeReference] public BlackboardVariable<State> State;

    protected override Status OnUpdate()
    {
		if (Hp.Value <= 0f)
		{
			State.Value = global::State.Die;
		}
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

