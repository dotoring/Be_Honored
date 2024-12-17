using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaTest", story: "[PizzaObj] 를 생성하고 [BossMonster] 를 준다", category: "Action", id: "c647d813db2399aee6063ab38da111db")]
public partial class PizzaTestAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> PizzaObj;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;

    protected override Status OnStart()
    {
		GameObject pizzaobj = (GameObject)GameObject.Instantiate(PizzaObj);
		pizzaobj.transform.parent = Self.Value.transform;
		pizzaobj.transform.localPosition = Vector3.zero+(Vector3.up*1);
		pizzaobj.transform.forward = Self.Value.transform.forward;
		pizzaobj.GetComponent<PizzaPattern>().InitThis(BossMonster.Value);

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

