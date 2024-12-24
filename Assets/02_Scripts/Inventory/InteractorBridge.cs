using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractorBridge : MonoBehaviour
{
	XRInteractionManager interactorManager;

	private void Awake()
	{
		interactorManager = GetComponent<XRInteractionManager>();
	}

	private void Start()
	{
		BagCtrl.Instance.SetInteractionMgr(interactorManager);
	}
}
