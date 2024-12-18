using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using System.Collections.Generic;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FireBallStart", story: "[BossMonster] 에서 플레이어리스트 를 가져와서 가장 먼 플레이어 N-1 에게 [FirePoint] , [FirePointList] 를 생성 [self]", category: "Action", id: "bbb1dc4213148c4c172825d166a89c4e")]
public partial class FireBallStart : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
    [SerializeReference] public BlackboardVariable<GameObject> FirePoint;
    [SerializeReference] public BlackboardVariable<List<GameObject>> FirePointList;
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    private List<GameObject> targetList;
    protected override Status OnStart()
    {
		targetList = BossMonster.Value.playerList;
		float mindis = 99.0f;
		GameObject remove=new GameObject();
		foreach (var item in BossMonster.Value.playerList)
		{
			float dis = Vector3.SqrMagnitude(item.transform.position - Self.Value.transform.position);
			if(mindis>dis)
			{
				mindis = dis;
				remove= item;
			}
		}
		targetList.Remove(remove);
		foreach (var item in targetList)
		{
			GameObject Point = GameObject.Instantiate(FirePoint.Value,item.transform.position,Quaternion.identity);
			FirePointList.Value.Add(Point);
		}


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

