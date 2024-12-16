using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossDie", story: "[hp] 로 [BossState] 를 변경한다", category: "Action", id: "cca2c086a709ca6c74ef8a18ab6c252d")]
public partial class BossDieAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Hp;
    [SerializeReference] public BlackboardVariable<BossState> BossState;

    protected override Status OnStart()
    {
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

