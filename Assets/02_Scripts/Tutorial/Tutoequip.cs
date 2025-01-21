using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Tutoequip : MonoBehaviour
{
    protected XRGrabInteractable xRGrabInteractable;
    private bool isOnSlot;
    public Action equip;

    private void Awake()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        xRGrabInteractable.selectExited.AddListener((args) =>
        {
            if (isOnSlot)
            {
                PutItOn();
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            isOnSlot = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            isOnSlot = false;
        }
    }

    private void PutItOn()
    {
        equip.Invoke();
        Destroy(gameObject);
    }
}