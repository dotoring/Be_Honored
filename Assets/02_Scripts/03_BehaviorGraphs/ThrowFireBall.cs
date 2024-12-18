using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ThrowFireBall", story: "[BossMonster] 에서 FireStartPoint 를 가져와 [FireBallObj] 를 [FirePointList] 에 날린다", category: "Action", id: "611dda86cda555e96b5680dda3e24302")]
public partial class ThrowFireBall : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<GameObject> FireBallObj;
    [SerializeReference] public BlackboardVariable<List<GameObject>> FirePointList;
    protected override Status OnStart()
    {
		for (int i = 0; i < FirePointList.Value.Count; i++)
		{
			GameObject ball =(GameObject)GameObject.Instantiate(FireBallObj, BossMonster.Value.fireStartPoint[i].transform.position,Quaternion.identity);
			ball.GetComponent<FireBall>().InitBall(BossMonster.Value, FirePointList.Value[i], BossMonster.Value.fireStartPoint[i]);
		}
		FirePointList.Value.Clear();

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

