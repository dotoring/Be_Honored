using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartMoveAni", story: "[Self] 의 [MoveAni] 를 [Animation] 으로 실행한다", category: "Action", id: "48afd30a18b3eea255202637c25c8513")]
public partial class StartMoveAniAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<AnimationClip> MoveAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;

    protected override Status OnStart()
    {
		MoveAni.Value.wrapMode = WrapMode.Loop;
		Animation.Value.Play("run");
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

