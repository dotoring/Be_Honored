using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ScrapItem : MonoBehaviour
{
	XRGrabInteractable xRGrabInteractable;

	private void Awake()
	{
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
	}

	private void Start()
	{

		xRGrabInteractable.selectEntered.AddListener(grabed);

	}



	private void grabed(SelectEnterEventArgs arg0)
	{
		Debug.Log($" selected ");
		App.Instance.inventory.Add(this.name);
		Destroy(this.gameObject);
	}
}
