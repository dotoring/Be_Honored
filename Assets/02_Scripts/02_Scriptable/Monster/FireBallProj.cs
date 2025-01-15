using UnityEngine;

public class FireBallProj : MonoBehaviour
{
	[SerializeField] private Vector3 dis;
	[SerializeField] private float speed;
	[SerializeField] private float power;

	private void Update()
	{
		
	}

	private void OnTriggerEnter(Collider other)
	{
		
	}

	public void InitProj(Vector3 forward, float attackPower)
	{
		dis = forward;
		power= attackPower;
	}
}
