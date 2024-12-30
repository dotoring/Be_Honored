using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LazerEnd", story: "[Self] 의 전방을 [Player] 방향 으로 다시 바꿈", category: "Action", id: "86d492810efa8be02ed7eb1ca6a843f7")]
public partial class LazerEnd : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    protected override Status OnStart()
    {
		
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
		Debug.Log("원래대로 돌리는중");
		Self.Value.transform.forward = Vector3.Lerp(Self.Value.transform.forward, (Player.Value.transform.position - Self.Value.transform.position).normalized, 1f*Time.deltaTime);
		return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

