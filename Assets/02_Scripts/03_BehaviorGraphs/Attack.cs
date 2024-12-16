using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[Self] 의 [AttackAni] 를 [Animation] 으로 실행한다", category: "Action", id: "9ba506786b29c946d202813922f5d0c2")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> AttackAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;
    protected override Status OnStart()
    {
		AttackAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("attack");
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

