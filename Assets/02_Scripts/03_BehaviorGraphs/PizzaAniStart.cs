using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaAniStart", story: "[Self] [PizzaAni] [Animation]", category: "Action", id: "70dd21f29cd9db4c1db3fa47080771e4")]
public partial class PizzaAniStartAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> PizzaAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;
    protected override Status OnStart()
    {
		PizzaAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("PizzaPattern");
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

