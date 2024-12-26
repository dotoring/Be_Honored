using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorBridge : MonoBehaviour
{
	XRInteractionManager interactorManager;

	private void Awake()
	{
		interactorManager = GetComponent<XRInteractionManager>();

		if (App.Instance.interactorManager == null)
		{
			App.Instance.interactorManager = new Observable<XRInteractionManager>(interactorManager);
		}
		else
		{
			App.Instance.interactorManager.Value = interactorManager;
		}
	}
}
