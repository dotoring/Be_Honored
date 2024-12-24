using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitCerberus", story: "[Self] 에 있는 [BossMonster] 를 이용해 [DetectRange] 와 [AttackRange] 와 [AttackPower] , [Hp] 를 초기화한다", category: "Action", id: "dc4fc02844ca325f957613ca730a8353")]
public partial class InitCerberusAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<float> DetectRange;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<float> AttackPower;
    [SerializeReference] public BlackboardVariable<float> Hp;
    protected override Status OnStart()
    {
		BossMonster.Value	= Self.Value.GetComponent<BossMonster>();
		DetectRange.Value	= BossMonster.Value.detectRange;
		AttackRange.Value	= BossMonster.Value.attackRange;
		AttackPower.Value	= BossMonster.Value.attackPower;
		Hp.Value			= BossMonster.Value.hp;
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

