using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Test", story: "[self] 가 죽으면 [Monster] 내부의 spawner 에게 리스트에서 삭제하라고 시킴", category: "Action", id: "7f8ff5642a4b2b8f3810cd7f86689204")]
public partial class TestAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Monster> Monster;
    protected override Status OnStart()
	{
		Monster.Value.dieEvent.Invoke();
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

