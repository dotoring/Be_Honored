using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemSeller : MonoBehaviour
{
	public int price;
	public XRGrabInteractable xrGrab;

	private void Start()
	{
		xrGrab.selectEntered.AddListener(OnSelectEntered);
	}

	private void OnDestroy()
	{
		xrGrab.selectEntered.RemoveListener(OnSelectEntered);
	}

	private void OnSelectEntered(SelectEnterEventArgs args)
	{
		if (price > App.Instance.gold.Value)
		{
			//잡기 이벤트만 발생시키고 잡기 해제
			xrGrab.interactionManager.CancelInteractableSelection(args.interactableObject);
		}
		else
		{
			App.Instance.UseGold(price);
			Destroy(this);
		}
	}
}
