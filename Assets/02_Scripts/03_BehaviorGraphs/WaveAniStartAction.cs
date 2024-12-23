using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "WaveAniStart", story: "[Self] [WaveAni] [Animation]", category: "Action", id: "3256e6164cb46bc0c2e05b0955e76418")]
public partial class WaveAniStartAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> WaveAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		WaveAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("WavePattern");
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

