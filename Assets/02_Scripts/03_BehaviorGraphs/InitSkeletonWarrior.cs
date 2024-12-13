using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEditor;
using Unity.VisualScripting;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitSkeletonWarrior", story: "[SkeletonWarriorData] 를 이용해 [DectetRange] 와 [AttackRange] 와 [AttackPower] , [Hp] 를 초기화한다", category: "Action", id: "28a30f8882ca58828c7681cabe6d1226")]
public partial class InitSkeletonWarrior : Action
{
    [SerializeReference] public BlackboardVariable<SkeletonWarrior> SkeletonWarriorData;
    [SerializeReference] public BlackboardVariable<float> DectetRange;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<float> AttackPower;
    [SerializeReference] public BlackboardVariable<float> Hp;
    protected override Status OnStart()
    {
		DectetRange.Value = SkeletonWarriorData.Value.detectRange;
		AttackRange.Value = SkeletonWarriorData.Value.attackRange;
		AttackPower.Value = SkeletonWarriorData.Value.attackPower;
		Hp.Value = SkeletonWarriorData.Value.hp;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }
}

