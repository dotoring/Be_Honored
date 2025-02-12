using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class tutoswordholder : MonoBehaviour
{
    
    public bool isEmpty;
   public XRGrabInteractable swordGI;
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
        if (isEmpty) return;
        //잡기 이벤트만 발생시키고 잡기 해제
        xrGrab.interactionManager.CancelInteractableSelection(args.interactableObject);
        swordGI.interactionManager.SelectEnter(args.interactorObject, swordGI);

      
    }
 
}
