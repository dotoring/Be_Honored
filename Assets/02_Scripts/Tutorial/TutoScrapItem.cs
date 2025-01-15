using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TutoScrapItem : ScrapItem
{
    public Action inbag; 

    private void Awake()
    {
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        action += SetInteractionMgr;
        xRGrabInteractable.selectEntered.AddListener((args) => { SetPhysicsInGrab(false); });
        xRGrabInteractable.selectExited.AddListener((args) =>
        {
            SetPhysicsInGrab(true);
            CheckInBag(); 
        });
    }


    public void SetInteractionMgr(XRInteractionManager mgr)
    {
        xRGrabInteractable.interactionManager = mgr;
    }

    protected void SetPhysicsInGrab(bool b)
    {
        col.isTrigger = !b;
        rb.isKinematic = !b;
        rb.useGravity = b;
    }
    
    protected virtual void CheckInBag()
    {
        if (isInBag)
        {
            if(!bagCtrl.IsInBag(this))
            {
                if (bagCtrl.CheckWeight(this))
                {
                    SetInBag();
                    bagCtrl.AddScrap(this);
                }
            }
            else
            {
                SetInBag();
            }
        }
        else
        {
            PullOut();
            if(bagCtrl != null)
            {
                bagCtrl.RemoveScrap(this);
            }
        }
    }
    
    protected void SetInBag()
    {
        

        xRGrabInteractable.throwOnDetach = false;
        rb.isKinematic = true;
        rb.useGravity = false;

        transform.SetParent(bag.transform, true);
        inbag.Invoke();
        
    }
    
    protected void PullOut()
    {
        

        xRGrabInteractable.throwOnDetach = true;
        rb.isKinematic = false;
        rb.useGravity = true;

        transform.SetParent(null, true);
    }
    
}