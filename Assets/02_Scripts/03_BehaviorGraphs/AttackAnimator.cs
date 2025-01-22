using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Photon.Pun;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackAnimator", story: "[monster]", category: "Action", id: "c1df31dcbb67a95c1e6a126a4cb88b3a")]
public partial class AttackAnimatorAction : Action
{
    [SerializeReference] public BlackboardVariable<Monster> Monster;
    protected override Status OnStart()
    {
		if (PhotonNetwork.IsConnected)
			Monster.Value.pv.RPC(nameof(Monster.Value.AttackAniRPC), RpcTarget.All, true);
		else
			Monster.Value.ani.SetBool("Attack",true);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
		Monster.Value.pv.RPC(nameof(Monster.Value.AttackAniRPC), RpcTarget.All, false);
	}
}

