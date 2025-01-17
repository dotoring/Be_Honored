using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room1 : MonoBehaviour
{
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] TutoBagPortal tutobag;
    [SerializeField] TutoScrapItem scrap;
    [SerializeField] private GameObject nextRoomBtnText;

    private void Start()
    {
        text.text = "This room is for UI";

        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        pokeButton.enabled = false;
        yield return new WaitForSeconds(3f);
        text.text = "You can opon Bag for you";
        yield return new WaitForSeconds(3f);
        text.text = "Tag your ball in left hand to ball in your arm";


        tutobag.opend += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        tutobag.opend -= ActionOnPerformed;
        NextStep = false;
        text.text = "Bag is for you, when you need bag, call bag by tag";
        //yield return new WaitForSeconds(3f);

        scrap.inbag += ActionOnPerformed;
        text.text = "Pick scrap in center of room, Put in bag";
        yield return new WaitUntil(() => NextStep);
        scrap.inbag -= ActionOnPerformed;
        NextStep = false;

        yield return new WaitForSeconds(3f);
        pokeButton.enabled = true;
        nextRoomBtnText.SetActive(true);
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