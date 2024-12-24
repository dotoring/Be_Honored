using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StompAniStart", story: "[Self] [StompAni] [Animation]", category: "Action", id: "835414a21c21f1c14d22c854e2068bce")]
public partial class StompAniStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> StompAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;
    protected override Status OnStart()
    {
		StompAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("StompPattern");
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

