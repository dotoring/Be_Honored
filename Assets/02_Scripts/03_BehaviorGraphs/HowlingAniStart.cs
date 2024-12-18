using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HowlingAniStart", story: "[Self] [HowlingAni] [Animation]", category: "Action", id: "f4bd8edb1dbe2e6c564cfcec675f4c9b")]
public partial class HowlingAniStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> HowlingAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		HowlingAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("HowlingPattern");
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

