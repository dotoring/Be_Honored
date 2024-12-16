using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartIdleAni", story: "[Self] 의 [idleAni] 를 [Animation] 로 실행한다", category: "Action", id: "7a1852d945c8a01a033a9ea91f10b295")]
public partial class StartIdleAniAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> IdleAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;
    protected override Status OnStart()
    {
		IdleAni.Value.wrapMode = WrapMode.Loop;
		Animation.Value.Play("idle");
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

