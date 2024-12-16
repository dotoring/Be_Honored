using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEditor;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitSkeletonWarrior", story: "[Self] 에 있는 [Monster] 를 이용해 [DectetRange] 와 [AttackRange] 와 [AttackPower] , [Hp] 를 초기화한다", category: "Action", id: "28a30f8882ca58828c7681cabe6d1226")]
public partial class InitSkeletonWarrior : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Monster> Monster;
    [SerializeReference] public BlackboardVariable<float> DectetRange;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<float> AttackPower;
    [SerializeReference] public BlackboardVariable<float> Hp;
    protected override Status OnStart()
    {
		Monster.Value=Self.Value.GetComponent<Monster>();
		DectetRange.Value = Monster.Value.detectRange;
		AttackRange.Value = Monster.Value.attackRange;
		AttackPower.Value = Monster.Value.attackPower;
		Hp.Value = Monster.Value.hp;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }
}

