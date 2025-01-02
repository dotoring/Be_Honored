using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabTest : MonoBehaviour
{
	GameObject parent;
	XRGrabInteractable interactable;

	private void Awake()
	{
		parent = transform.parent.gameObject;
		interactable = GetComponent<XRGrabInteractable>();
	}

	void Start()
    {
		interactable.selectEntered.AddListener((arg) =>
		{
			Destroy(parent);
		});
    }
}
