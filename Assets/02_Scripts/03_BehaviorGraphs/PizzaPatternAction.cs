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
		PizzaObj.Value.gameObject.SetActive(true);
		PizzaObj.Value.GetComponent<PizzaPattern>().InitThis(BossMonster.Value);
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

