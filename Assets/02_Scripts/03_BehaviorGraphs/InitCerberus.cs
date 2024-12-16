using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "InitCerberus", story: "[BossMonster] 를 [Self] 에 연결함", category: "Action", id: "dc4fc02844ca325f957613ca730a8353")]
public partial class InitCerberusAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
		BossMonster.Value = Self.Value.GetComponent<BossMonster>();
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

