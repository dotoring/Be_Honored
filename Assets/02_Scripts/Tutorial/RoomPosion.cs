using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RoomPosion : MonoBehaviour
{
    [SerializeField] TutoManager _manager; // 사운드클립있음
    [SerializeField] AudioSource audioSource; // xrorigin용
    [SerializeField] private XRSimpleInteractable pokeButton; // 다음방 가는 버튼
    [SerializeField] private TMP_Text text; // 텍스트 설명
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] private GameObject nextRoomBtnText;
    [SerializeField] private GameObject canvas;

    [SerializeField] private TutoUsableItem posion;
    [SerializeField] private tutoHPBar hpbtn;

    private void Start()
    {
        text.text = "Welcome to the New World";

        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitUntil(() => canvas.activeInHierarchy);
        Player.Instance.Damaged(20);
        audioSource.clip = _manager.audioClips[13];
        audioSource.Play();
        text.text = "이방은 던전에서 회복에 관한 방입니다. 던전 내에서는 포션을 사용하여 체력을 회복할수 있습니다.";
        pokeButton.enabled = false;
        yield return new WaitForSeconds(3f); // start logic end

        posion.inbag += ActionOnPerformed;
        audioSource.clip = _manager.audioClips[14];
        audioSource.Play();
        text.text = "방 앞쪽에 포션이 떨어져 있습니다. 해당 포션을 집어, 가방에 넣어보세요";
        yield return new WaitUntil(() => NextStep);
        posion.inbag -= ActionOnPerformed;
        NextStep = false;



		hpbtn.touch += ActionOnPerformed;
        audioSource.clip = _manager.audioClips[17];
        audioSource.Play();
        text.text = "왼팔을 들어 체력 상황을 확인해 보세요. 오른손으로 왼쪽팔의 체력바를 터치해 주세요";
        yield return new WaitUntil(() => NextStep);
hpbtn.touch -= ActionOnPerformed;

        NextStep = false;


        posion.posionTake += ActionOnPerformed;
        audioSource.clip = _manager.audioClips[15];
        audioSource.Play();
        text.text = "현재 체력이 절반입니다. 물약을 섭취해 봅시다. 물약을 들어 얼굴로 가져가세요";
        yield return new WaitUntil(() => NextStep);
        posion.posionTake -= ActionOnPerformed;
        NextStep = false;


        //text.text = "왼팔을 들어 체력 상황을 다시 확인해 보세요, 체력이 회복되었습니다.";


        // posion.posionTake += ActionOnPerformed;
        audioSource.clip = _manager.audioClips[16];
        audioSource.Play();
        text.text = "왼팔을 들어 변한 체력 상황을 확인해 보세요, 체력의 일정량이 회복되었습니다. 언제나 체력 상황을 신경쓰도록 합니다.";
        // yield return new WaitUntil(() => NextStep);
        //posion.posionTake -= ActionOnPerformed;
        // NextStep = false;


        pokeButton.enabled = true; // end logic start
        nextRoomBtnText.SetActive(true);
        text.text = "수고하셨습니다. 다음방 가는 버튼을 눌러 다음방 문을 열어보세요";
        pokeButton.selectEntered.AddListener(_ => { NextRoomSeq.SetActive(true); });
        audioSource.clip = _manager.audioClips[3];
        audioSource.Play();
        yield return new WaitForSeconds(3f);
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}
