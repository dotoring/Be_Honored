using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Photon.Pun;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "DestroySelf", story: "Photon으로 [Self] 파괴한다", category: "Action", id: "dde949eb275d62a6f94764c337fcdac5")]
public partial class DestroySelfAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    protected override Status OnStart()
    {
		PhotonNetwork.Destroy(Self.Value);
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

