using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossDie", story: "[Self] 가 죽으면 [BossMonster] 의 이벤트 발생 하고 [DieAni] 를 [Animation] 으로 실행", category: "Action", id: "48aa83f37e626303a7e678f5cf3bd248")]
public partial class BossDieAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<AnimationClip> DieAni;
    [SerializeReference] public BlackboardVariable<Animation> Animation;
    protected override Status OnStart()
    {
		DieAni.Value.wrapMode = WrapMode.Once;
		Animation.Value.Play("death");
		BossMonster.Value.dieEvent.Invoke();
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

