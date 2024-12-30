using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		transform.LookAt(Camera.main.transform.position);
	}
}
