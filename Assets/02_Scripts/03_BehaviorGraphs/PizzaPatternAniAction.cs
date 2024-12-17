using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaPatternAni", story: "[Self] 의 [PizzaAni] 를 [Animation] 을 실행한다", category: "Action", id: "31d04283b939039f7581990f9162c6fd")]
public partial class PizzaPatternAniAction : Action
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

