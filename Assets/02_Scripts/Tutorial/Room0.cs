using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room0 : MonoBehaviour
{
    [SerializeField] TutoManager _manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] InputActionProperty move;
    [SerializeField] InputActionProperty turn;
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] private GameObject nextRoomBtnText;
    
    private void Start()
    {
        //text.text = "Welcome to the New World";
        text.text = "새로운 세계에 오신것을 환영합니다.";
        
        
        

        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.clip = _manager.audioClips[0];
        audioSource.Play();
        pokeButton.enabled = false;
        yield return new WaitForSeconds(3f);
        text.text = "이곳은 튜토리얼 공간입니다.";
        yield return new WaitForSeconds(3f);
        audioSource.clip = _manager.audioClips[1];
        audioSource.Play();
        text.text = "우선 왼손 엄지로 조이스틱을 움직여서 케릭터를 이동시켜 보세요";
        yield return new WaitForSeconds(3f);
        //text.text = "Move Your charicter";

        move.action.performed += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        move.action.performed -= ActionOnPerformed;
        NextStep = false;
        
        
        audioSource.clip = _manager.audioClips[2];
        audioSource.Play();
        text.text = "잘하셨습니다. 오른손의 조이스틱을 움직여 케릭터를 회전시켜 보세요";
        //text.text = "You can Trun by right Hand Thumbstick";
        turn.action.performed += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        turn.action.performed -= ActionOnPerformed;
        NextStep = false;
        pokeButton.enabled = true;
        nextRoomBtnText.SetActive(true);
        
        audioSource.clip = _manager.audioClips[3];
        audioSource.Play();
        text.text = "수고하셨습니ㅏㄷ. 다음방 가는 버튼을 눌러 다음방 문을 열어보세요";
        //text.text = "You can Go next room by click button";
        pokeButton.selectEntered.AddListener(_ => { NextRoomSeq.SetActive(true); });

        yield return new WaitForSeconds(3f);
    }


    private void ActionOnPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}