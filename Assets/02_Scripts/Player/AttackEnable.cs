using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AttackEnable : MonoBehaviour
{
	XRGrabInteractable xRGrabInteractable;
	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		xRGrabInteractable.selectEntered.AddListener(args =>
		{
			args.interactorObject.transform.GetComponent<NearFarInteractor>()
					.selectActionTrigger =
				XRBaseInputInteractor.InputTriggerType.Toggle;
			Player.Instance.Armed.Invoke();
		});
		xRGrabInteractable.selectExited.AddListener(args =>
		{
			args.interactorObject.transform.GetComponent<NearFarInteractor>()
			.selectActionTrigger =
				XRBaseInputInteractor.InputTriggerType.StateChange;
			Player.Instance.UnArmed.Invoke();
		});

	}

	private void OnDisable()
	{
		xRGrabInteractable.selectEntered.RemoveAllListeners();
		xRGrabInteractable.selectExited.RemoveAllListeners();
	}



}
