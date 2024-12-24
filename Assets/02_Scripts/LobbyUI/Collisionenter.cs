using System;
using UnityEngine;

public class Collisionenter : MonoBehaviour
{
	[SerializeField] GameObject canvas;

	private void Awake()
	{
		CanvasActive(false);
	}
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log($" {other.name} collisions");
		CanvasActive(true);
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name.Equals("XR Origin (VR)"))
		{
			CanvasActive(false);
		}
	}
	private void CanvasActive(bool v)
	{
		canvas.SetActive(v);
	}

	// private void LateUpdate()
	// {
	// 	canvas.transform.LookAt(Camera.main.transform.position);
	// }


}
