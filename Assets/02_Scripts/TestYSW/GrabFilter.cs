using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabFilter : MonoBehaviour
{
	public bool canGrab;
	public XRBaseInteractable xrGrab;

	private void Start()
	{
		xrGrab.selectEntered.AddListener(onSelectEntered);
	}

	private void onSelectEntered(SelectEnterEventArgs args)
	{
		if (!canGrab)
		{
			//잡기 이벤트만 발생시키고 잡기 해제
			xrGrab.interactionManager.CancelInteractableSelection(args.interactableObject);

			Debug.Log("can not grab");
		}
	}
}
