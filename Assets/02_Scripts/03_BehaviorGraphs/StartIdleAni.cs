using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Photon.Pun;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "StartIdleAni", story: "[BossMonster] , [ResetPos]", category: "Action", id: "7a1852d945c8a01a033a9ea91f10b295")]
public partial class StartIdleAniAction : Action
{
    [SerializeReference] public BlackboardVariable<BossMonster> BossMonster;
	[SerializeReference] public BlackboardVariable<Transform> ResetPos;
    protected override Status OnStart()
    {
		ResetPos.Value = BossMonster.Value.resetPos;
		bool check;
		check= (Vector3.Distance(BossMonster.Value.transform.position, ResetPos.Value.transform.position)<0.5f);
		if (check)
			BossMonster.Value.StartAnimationRPC("idle");
		else
			BossMonster.Value.StartAnimationRPC("run");
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

