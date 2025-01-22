using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room2 : MonoBehaviour
{
    [SerializeField] TutoManager _manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] TutoSandBag sandBag;
    [SerializeField] private GameObject nextRoomBtnText;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        //text.text = "이방은 전투에 관한 방입니다. 오른손 ";
        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitUntil(() => canvas.activeInHierarchy);
        text.text = "이방은 전투에 관한 방입니다. 오른손 그랩버튼으로 무기를 집어드세요";
        audioSource.clip = _manager.audioClips[7];
        audioSource.Play();
        pokeButton.enabled = false;
        Player.Instance.Armed += ActionOnPerformed;
        yield return new WaitForSeconds(3f);
        yield return new WaitForSeconds(3f);
        //text.text = "Pick up waepon by Right Grab Button";

        yield return new WaitUntil(() => NextStep);
        Player.Instance.Armed -= ActionOnPerformed;
        NextStep = false;
        sandBag.WorkingOn();
        sandBag.die += ActionOnPerformed;
        text.text = "무기를 눈앞에 보이는 화살표를 따라 휘둘러주세요 그리고 적을 무찌르세요";
        yield return new WaitForSeconds(3f);
        audioSource.clip = _manager.audioClips[8];
        audioSource.Play();
        //text.text = "then Attack enemy in right of you";
        //yield return new WaitWhile(() => sandBag.hp > 0.3 );
        yield return new WaitUntil(() => NextStep);
        NextStep = false;
        sandBag.die -= ActionOnPerformed;
        pokeButton.enabled = true;
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