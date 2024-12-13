using Unity.Behavior;
using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
	[SerializeField]private Transform target;
	[SerializeField] private BehaviorGraphAgent aa;
	[SerializeField]private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance=Vector3.Distance(transform.position, target.position);
    }

	private void OnDrawGizmos()
	{
		if (distance > 2)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, target.position);
	}
}
