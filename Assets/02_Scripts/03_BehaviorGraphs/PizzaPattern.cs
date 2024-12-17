using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PizzaPattern", story: "[Self] 의 전방 [angle] , [radius] 부채꼴 범위 [floor] 를 나타낸다.", category: "Action", id: "812ca18f8615bfa7ca2cbe55820e1d44")]
public partial class PizzaPatternAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> Angle;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<GameObject> Floor;
    protected override Status OnStart()
    {
		Floor.Value = Self.Value.GetComponent<BossMonster>().CreateSectorMesh(Radius.Value, Angle.Value, 30);
	    Floor.Value.transform.parent = Self.Value.transform;
		Floor.Value.transform.localPosition = Vector3.zero;
		Floor.Value.transform.localRotation= Quaternion.identity;
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

