using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RoomArmory : MonoBehaviour
{
    [SerializeField] TutoManager _manager;  // 사운드클립있음
    [SerializeField] AudioSource audioSource; // xrorigin용
    [SerializeField] private XRSimpleInteractable pokeButton;  // 다음방 가는 버튼
    [SerializeField] private TMP_Text text; // 텍스트 설명
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] private GameObject nextRoomBtnText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private XRBaseInteractable grab;
    [SerializeField] private Tutoequip tutoequip;
    private void Start()
    {
        text.text = "Welcome to the New World";


        nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitUntil(() => canvas.activeInHierarchy);
        audioSource.clip = _manager.audioClips[10];
        audioSource.Play();
        text.text = "이방은 장비에 관한 방입니다. 왼쪽에 바지가 있습니다. 장비는 적을 처치시 획득할수있습니다. 장비를 들어보세요";
        pokeButton.enabled = false;
        yield return new WaitForSeconds(3f);  // 시작 루틴
        
        //집어 올리고
        grab.selectEntered.AddListener( _ => ActionOnPerformed());
        
        yield return new WaitUntil(() => NextStep);
        grab.selectEntered.RemoveAllListeners();
        NextStep = false;
        
        
        //장비를 장착하고 
        audioSource.clip = _manager.audioClips[11];
        audioSource.Play();
        text.text = "장비를 왼쪽 큐브모양의 슬롯에 넣어보세요 이곳은 튜토리얼이라 던전에서 장비를 입을수 있지만 실제 던전에서는 마을에 가셔야 합니다.";
        tutoequip.equip += ActionOnPerformed;
        yield return new WaitUntil(() => NextStep);
        tutoequip.equip -= ActionOnPerformed;
        NextStep = false;
        yield return new WaitForSeconds(1f);
        audioSource.clip = _manager.audioClips[12];
        audioSource.Play();
        text.text = "장비는 마을의 무기점에 가서 장비를 적합한 슬롯에 넣는것으로 장비할 수 있고, 그에따라 능력이 향상됩니다.";
        
        
        // 다음방 가는 루틴
        pokeButton.enabled = true;
        nextRoomBtnText.SetActive(true);
        text.text = "Great, Step in This room is over";
        text.text = "You can Go next room by click button";
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