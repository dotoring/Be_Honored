using System;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    public AudioClip[] audioClips;

    private void Start()
    {
	    Player.Instance.ChangeBGM(0);
    }
}
