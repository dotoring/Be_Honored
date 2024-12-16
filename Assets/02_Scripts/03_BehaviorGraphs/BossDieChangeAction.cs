using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BossDieChange", story: "[hp] 로 [BossState] 를 변경한다", category: "Action", id: "9490512546e121dae144b77ca6135d49")]
public partial class BossDieChangeAction : Action
{
    [SerializeReference] public BlackboardVariable<float> Hp;
    [SerializeReference] public BlackboardVariable<BossState> BossState;

    protected override Status OnStart()
    {
		if (Hp.Value < 0)
			BossState.Value = global::BossState.Die;
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

