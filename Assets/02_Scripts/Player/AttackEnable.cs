using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class AttackEnable : MonoBehaviour
{
	XRGrabInteractable xRGrabInteractable;
	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		xRGrabInteractable.selectEntered.AddListener(_ =>
		{
			Player.Instance.Armed.Invoke();
		});
		xRGrabInteractable.selectExited.AddListener(_ =>
		{
			Player.Instance.UnArmed.Invoke();
		});

	}

	private void OnDisable()
	{
		xRGrabInteractable.selectEntered.RemoveAllListeners();
		xRGrabInteractable.selectExited.RemoveAllListeners();
	}



}
