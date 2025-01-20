using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SwordCtrl : MonoBehaviour
{
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;
    // [SerializeField] private GameObject holder;
    private void Start()
    {
	    grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
	    grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
	    gameObject.transform.localPosition = Vector3.zero;
	    gameObject.transform.localRotation = Quaternion.identity;
    }

}
