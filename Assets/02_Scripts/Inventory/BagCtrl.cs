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
		App.Instance.Resetposition += DestroyBag;

		if(Instance != null && Instance != this)
		{
			Destroy(this.gameObject);
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		App.Instance.interactorManager.AddListener((mgr) =>
		{
			SetInteractionMgr(mgr);
		});
	}

	public void SetInteractionMgr(XRInteractionManager mgr)
	{
		xRGrabInteractable.interactionManager = mgr;
	}

	void DestroyBag()
	{
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		App.Instance.Resetposition -= DestroyBag;
	}
}
