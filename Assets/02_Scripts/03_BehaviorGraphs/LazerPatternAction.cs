using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LazerPatternAction", story: "[LazerObj] 를 소환하고 [self] [BossMonster] 를 주고 obj가 함께 [lazerRotateAngle] 로 돈다", category: "Action", id: "032ebf235d9f577ec6b24be9373bf7e4")]
public partial class LazerPatternAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> LazerObj;
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<float> LazerRotateAngle;
    protected override Status OnStart()
    {
		LazerObj.Value.SetActive(true);
		LazerObj.Value.transform.localPosition = Vector3.zero + (Vector3.up * -3);
		LazerObj.Value.GetComponent<LazerPattern>().InitThis(BossMonster.Value,LazerRotateAngle);
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

