using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FireBallAniStart", story: "[Self] [FireBallAni] [Animation]", category: "Action", id: "391901d0b85aa1afdc24f75f463439ee")]
public partial class FireBallAniStart : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> FireBallAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		FireBallAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("FireBallPattern");
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

