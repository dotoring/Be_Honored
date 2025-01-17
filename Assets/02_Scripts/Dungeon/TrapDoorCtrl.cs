using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TrapDoorCtrl : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] XRGrabInteractable grabInteractable;
	private bool isBreak;

	private void Start()
	{
		grabInteractable.selectExited.AddListener((_) =>
		{
			if (isBreak)
			{
				rb.useGravity = true;
			}
		});
	}

	private void OnJointBreak(float breakForce)
	{
		Debug.Log("Detected break force");
		isBreak = true;
	}
}
