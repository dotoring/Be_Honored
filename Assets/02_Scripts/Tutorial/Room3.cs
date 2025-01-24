using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Room3 : MonoBehaviour
{
    [SerializeField] TutoManager _manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private XRSimpleInteractable pokeButton;
    [SerializeField] private TMP_Text text;
    bool NextStep;
    [SerializeField] GameObject NextRoomSeq;
    [SerializeField] private GameObject canvas;
    
    private void Start()
    {
        text.text = "마지막 방입니다.";
        
        //nextRoomBtnText.SetActive(false);
        StartCoroutine(seq());
    }

    IEnumerator seq()
    {
        yield return new WaitUntil( ()  => canvas.activeInHierarchy);
        audioSource.clip = _manager.audioClips[9];
        audioSource.Play();
        //pokeButton.enabled = false;
        yield return null;
        yield return new WaitForSeconds(3);
        text.text = "모험을 위해 포탈로 들어가 새로운 모험을 시작하세요";
    }

    private void ActionOnPerformed()
    {
        Debug.Log("check the action");
        NextStep = true;
    }
}
