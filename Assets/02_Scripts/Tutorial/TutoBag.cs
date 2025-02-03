using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TutoBag : MonoBehaviour
{
    [SerializeField] GameObject bagPref;
    [SerializeField] XRGrabInteractable grabInteractable;
    [SerializeField] XRBaseInteractor interactor;
    [SerializeField] GameObject model;
    public Action opend;
    private void Start()
    {
        grabInteractable.selectEntered.AddListener(PickBag);

        grabInteractable.hoverEntered.AddListener(OnHover);
        grabInteractable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverExit(HoverExitEventArgs arg0)
    {
        model.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnHover(HoverEnterEventArgs arg0)
    {
        Debug.Log("OnHover.0");
        model.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    private void PickBag(SelectEnterEventArgs args)
    {
        //잡기 이벤트만 발생시키고 잡기 해제
        IXRSelectInteractable interactable = args.interactableObject;
        grabInteractable.interactionManager.CancelInteractableSelection(interactable);

        //가방이 없으면 생성
        if (BagCtrl.Instance == null)
        {
            Instantiate(bagPref, transform.position, Quaternion.identity);
        }

        XRGrabInteractable bagInteractable = BagCtrl.Instance.GetComponent<XRGrabInteractable>();
        if (bagInteractable == null) return;

        //가방 쥐어주기
        bagInteractable.interactionManager.SelectEnter(interactor, (IXRSelectInteractable)bagInteractable);
        opend?.Invoke();
        

    }
 
}
