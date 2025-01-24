using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room1 : MonoBehaviour
{
    [SerializeField] TutoManager _manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] TutoBagPortal tutobag;
    [SerializeField] TutoScrapItem scrap;
    [SerializeField] private GameObject nextRoomBtnText;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        text.text = "이 방은 물품을 보관하는 가방에 관한 방입니다.";

        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
    yield return new WaitUntil( ()  => canvas.activeInHierarchy);
        audioSource.clip = _manager.audioClips[4];
        audioSource.Play();
        pokeButton.enabled = false;
        // yield return new WaitForSeconds(3f);
        // text.text = "You can opon Bag for you";
        //yield return new WaitForSeconds(3f);
        text.text = "오른 손목에 있는 회색공을 왼손 컨트롤러 중지 그랩버튼으로 집어보세요";


        tutobag.opend += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        tutobag.opend -= ActionOnPerformed;
        NextStep = false;
        
        audioSource.clip = _manager.audioClips[5];
        audioSource.Play();
        text.text = "이가방은 개인소유입니다. 가방이 필요할땐 언제라도 불러낼수 있습니다. 사망시 가방내 물품이 사라지니 조심하세요";
        //yield return new WaitForSeconds(3f);

        scrap.inbag += ActionOnPerformed;
        audioSource.clip = _manager.audioClips[6];
        audioSource.Play();
        text.text = "앞쪽 황금잔을 집어봅시다. 물품에 컨트롤러의 선을 가져다 대고 그랩버튼으로 집어 가방에 넣어보세요";
        yield return new WaitUntil(() => NextStep);
        scrap.inbag -= ActionOnPerformed;
        NextStep = false;

        //yield return new WaitForSeconds(3f);
        pokeButton.enabled = true;
        nextRoomBtnText.SetActive(true);
        text.text = "수고하셧습니다. 다음방 가는 버튼을 눌러 다음방 문을 열어보세요";
        pokeButton.selectEntered.AddListener(_ => { NextRoomSeq.SetActive(true); });
        audioSource.clip = _manager.audioClips[3];
        audioSource.Play();

        //yield return new WaitForSeconds(3f);
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}