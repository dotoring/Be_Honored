using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room2 : MonoBehaviour
{
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] TutoSandBag sandBag;

    private void Start()
    {
        text.text = "This room is for Arm";

        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitForSeconds(3f);
        text.text = "This room is for Arm";
        yield return new WaitForSeconds(3f);
        text.text = "Pick up waepon by Right Grab Button";
        
        Player.Instance.Armed += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        Player.Instance.Armed -= ActionOnPerformed;
        NextStep = false;
        // sandBag.die += ActionOnPerformed;
        text.text = "Good, you can see three ball infront of you";
        yield return new WaitForSeconds(3f);
        
        text.text = "then Attack enemy in right of you";
        yield return new WaitWhile(() => sandBag.hp > 0.3 );
        // NextStep = false;
        // sandBag.die -= ActionOnPerformed;
        
        text.text = "Great, Step in This room is over";
        text.text = "You can Go next room by click button";
        pokeButton.selectEntered.AddListener(_ => { NextRoomSeq.SetActive(true); });

        yield return new WaitForSeconds(3f);
        
        
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}