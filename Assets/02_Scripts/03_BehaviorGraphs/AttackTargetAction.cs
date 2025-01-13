using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "AttackTarget", story: "[Player] damaged by [Attackpower]", category: "Action", id: "d663da3728310572b997c637f1f2d805")]
public partial class AttackTargetAction : Action
{
	[SerializeReference] public BlackboardVariable<Transform> Player;
	[SerializeReference] public BlackboardVariable<GameObject> Self;
	[SerializeReference] public BlackboardVariable<float> Attackpower;

	protected override Status OnStart()
	{
		
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

