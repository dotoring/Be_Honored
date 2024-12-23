using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "RoarAniStart", story: "[Self] [RoarAni] [Animation]", category: "Action", id: "a5ade7450524519e90279fbf62dbfa95")]
public partial class RoarAniStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> RoarAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		RoarAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("roar");
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

