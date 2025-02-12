using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SwordHolder : MonoBehaviour
{
    //칼이 홀더에 있을 때 홀더를 잡으면 칼을 쥐어주기
    //칼이 홀더에 없을 때 작동 안하기
    //그냥 칼을 잡아서 가져갈때 홀더에 칼 있음 끄기

    public bool isEmpty;
    [SerializeField] private XRGrabInteractable swordGI;
    [SerializeField] XRGrabInteractable xrGrab;
    [SerializeField] private PhotonView swordPv;
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
	    if (swordPv.IsMine)
	    {
		    swordGI.interactionManager.SelectEnter(args.interactorObject, swordGI);
	    }
    }
}
