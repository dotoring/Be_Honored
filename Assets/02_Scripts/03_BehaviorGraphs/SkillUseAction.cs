using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SkillUse", story: "랜덤으로 [BossSkills] 를 발동시킴", category: "Action", id: "ca63998d8a38d39693297503a29a1027")]
public partial class SkillUseAction : Action
{
    [SerializeReference] public BlackboardVariable<BossSkills> BossSkills;

    protected override Status OnStart()
    {
		BossSkills.Value=(BossSkills)UnityEngine.Random.Range(0, 4);
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

