using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BagCtrl : MonoBehaviour
{
	public static BagCtrl Instance;
	XRGrabInteractable xRGrabInteractable;

	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();

		if(Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void SetInteractionMgr(XRInteractionManager mgr)
	{
		xRGrabInteractable.interactionManager = mgr;
	}
}
