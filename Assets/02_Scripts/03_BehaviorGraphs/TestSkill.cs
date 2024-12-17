using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "TestSkill", story: "스킬을 사용이 끝나면 [BossMonster] 에 신호를 줌", category: "Action", id: "a13d0f0bc7715ccc5f113a34a5ce318f")]
public partial class TestSkillAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;

    protected override Status OnStart()
    {
		BossMonster.Value.ResetSkill();
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

