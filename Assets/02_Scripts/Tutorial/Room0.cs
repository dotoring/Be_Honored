using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room0 : MonoBehaviour
{
    [SerializeField] InputActionProperty move;
    [SerializeField] InputActionProperty turn;
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] private GameObject nextRoomBtnText;

    private void Start()
    {
        text.text = "Welcome to the New World";

    nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        pokeButton.enabled = false;
        yield return new WaitForSeconds(3f);
        text.text = "This is Tutorial for BeHorned";
        yield return new WaitForSeconds(3f);
        text.text = "At First, You can Move by Left Hand Thumbstick";
        yield return new WaitForSeconds(3f);
        text.text = "Move Your charicter";

        move.action.performed += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        move.action.performed -= ActionOnPerformed;
        NextStep = false;


        text.text = "Good Job, then Trun your charicter";
        text.text = "You can Trun by right Hand Thumbstick";
        turn.action.performed += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        turn.action.performed -= ActionOnPerformed;
        NextStep = false;
        pokeButton.enabled = true;
    nextRoomBtnText.SetActive(true);
        text.text = "Great, Step in This room is over";
        text.text = "You can Go next room by click button";
        pokeButton.selectEntered.AddListener(_ => { NextRoomSeq.SetActive(true); });

        yield return new WaitForSeconds(3f);
    }


    private void ActionOnPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}