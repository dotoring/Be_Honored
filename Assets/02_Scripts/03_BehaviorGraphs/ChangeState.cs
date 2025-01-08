using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChangeState", story: "[Self] 주변에 [DectetRange] 의 안에 [PlayerLayer] 가 들어오면 [Player] 를 감지하고 Player 와 [distance] 를 계산하여 [AttackRange] 이용해 [state] 를 변경", category: "Action", id: "b6f3bd6f988eb30dfc6d68e28c733e65")]
public partial class ChangeStateAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<float> DectetRange;
    [SerializeReference] public BlackboardVariable<int> PlayerLayer;
    [SerializeReference] public BlackboardVariable<Transform> Player;
    [SerializeReference] public BlackboardVariable<float> Distance;
    [SerializeReference] public BlackboardVariable<float> AttackRange;
    [SerializeReference] public BlackboardVariable<State> State;
    protected override Status OnUpdate()
    {
		if(Player.Value!= null) 
			Distance.Value = Vector3.Distance(Player.Value.position, Self.Value.transform.position);


		Collider[] cols = Physics.OverlapSphere(Self.Value.transform.position, DectetRange, 1<<PlayerLayer);
		float min = DectetRange+1.0f;
		for(int i=0;i< cols.Length;i++)
		{
			float dis=Vector3.Distance(Self.Value.transform.position, cols[i].transform.position);
			if(min>dis)
			{
				Player.Value = cols[i].transform;
				min = dis;

			}
		}
		if (Player.Value == null)
			State.Value = global::State.Idle;
		else
		{
			if (AttackRange >= Vector3.Distance(Player.Value.position, Self.Value.transform.position))
			{
				Self.Value.GetComponent<Animator>().SetTrigger("AttackReady");
				State.Value = global::State.Attack;
			}
			else
			{
				State.Value = global::State.Move;
			}
		}
		return Status.Success;
    }
}

