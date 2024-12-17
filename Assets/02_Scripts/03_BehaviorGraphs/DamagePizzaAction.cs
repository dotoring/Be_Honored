using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Unity.VisualScripting.FullSerializer;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DamagePizza", story: "[floor] 위에 남아있는 [Player] 에게 [Damage] 를 준다", category: "Action", id: "6b72373313bc4b1999f065dbab9de6d7")]
public partial class DamagePizzaAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Floor;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<float> Damage;

    protected override Status OnStart()
    {
		GameObject.Destroy(Floor.Value);
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

