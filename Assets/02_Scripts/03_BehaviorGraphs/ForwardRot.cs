using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ForwardRot", story: "[self] , [player]", category: "Action", id: "233b43b1546b62b9474a3168c10b7d16")]
public partial class ForwardRotAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    protected override Status OnStart()
    {
		Transform tr=Self.Value.transform;
		Vector3 temp= (Player.Value.position - tr.position).normalized;
		temp.y = 0;
		tr.forward = temp;

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

