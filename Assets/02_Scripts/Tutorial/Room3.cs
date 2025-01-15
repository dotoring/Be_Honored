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
    

    private void Start()
    {
        text.text = "This room is Last Room";

        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return null;
        yield return new WaitForSeconds(3);
        text.text = "Move to Cube For Adventure";
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}