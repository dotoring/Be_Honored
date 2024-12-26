using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "HowlingAction", story: "[Self] 의 주변에 Playerlayer 에게 데미지를 준다", category: "Action", id: "7d975a45f0617f7001153e41542e8907")]
public partial class HowlingAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;

    protected override Status OnStart()
    {
		Collider[] cols = Physics.OverlapSphere(Self.Value.transform.position, 7.0f, 1 << 10);
		for(int i=0;i<cols.Length;i++)
		{
			cols[i].GetComponent<Player>()?.Damaged(BossMonster.Value.attackPower);
		}
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

