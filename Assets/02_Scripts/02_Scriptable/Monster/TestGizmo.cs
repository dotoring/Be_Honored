using UnityEngine;

public class TestGizmo : MonoBehaviour
{
	[SerializeField] private GameObject player;
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,transform.position+transform.forward*10);

		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, player.transform.position);
	}
}
