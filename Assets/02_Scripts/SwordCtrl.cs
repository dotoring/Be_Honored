using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SwordCtrl : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabInteractable;
	[SerializeField] SwordHolder swordHolder;

    private void Start()
    {
	    grabInteractable.selectEntered.AddListener(OnSelectEntered);
	    grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
	    grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
	    grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
	    swordHolder.isEmpty = true;
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
	    swordHolder.isEmpty = false;
	    gameObject.transform.localPosition = Vector3.zero;
	    gameObject.transform.localRotation = Quaternion.identity;
    }

}
