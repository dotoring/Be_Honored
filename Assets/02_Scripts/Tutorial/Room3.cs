using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room3 : MonoBehaviour
{
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    //[SerializeField] private GameObject nextRoomBtnText;
    
    private void Start()
    {
        text.text = "This room is Last Room";
        //nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        //pokeButton.enabled = false;
        yield return null;
        yield return new WaitForSeconds(3);
        text.text = "Move to Potal For Adventure";
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}
