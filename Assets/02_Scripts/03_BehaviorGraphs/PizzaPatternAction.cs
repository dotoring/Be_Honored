using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaPatternAction", story: "[PizzaObj] 를 생성하고 [BossMonster] 를 준다", category: "Action", id: "c647d813db2399aee6063ab38da111db")]
public partial class PizzaTestAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> PizzaObj;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    protected override Status OnStart()
    {
		BossMonster.Value.StartAnimationRPC("PizzaPattern");
		//모든 클라에서 오브젝트를 setactive하고 마클을 따라가게?
		PizzaObj.Value.gameObject.SetActive(true);
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

