using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaAniStart", story: "[BossMonster]", category: "Action", id: "70dd21f29cd9db4c1db3fa47080771e4")]
public partial class PizzaAniStartAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("PizzaPattern");
		BossMonster.Value.StartPatternRPC(0);
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

