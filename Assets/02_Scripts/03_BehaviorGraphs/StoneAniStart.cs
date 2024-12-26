using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StoneAniStart", story: "[self] [stoneAni] [Animation]", category: "Action", id: "e9ad1b10f554b4a62a507ba22b768d1c")]
public partial class StoneAniStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> StoneAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		StoneAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("StonePattern");
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

