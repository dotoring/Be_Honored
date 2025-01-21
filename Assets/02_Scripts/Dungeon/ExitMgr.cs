using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;
using System;

public class ExitMgr : MonoBehaviourPunCallbacks
{
	[SerializeField] Image loadingbar;
	[SerializeField] GameObject canvas;
	public static Action OnExitDungeon;
	[SerializeField] Transform underStage;
	[SerializeField] GameObject player;

	[SerializeField] AudioClip bossScreem;

	public void ExitDungeon()
	{
		OnExitDungeon?.Invoke();
		PhotonNetwork.LeaveRoom();
	}

	

	public void TeleportToUnderStage()
	{
		player.transform.position = underStage.position;
		Player.Instance.ChangeBGM(2);
		Player.Instance.audioSource.PlayOneShot(bossScreem);
	}

	private void OnDestroy()
	{
		OnExitDungeon = null;
	}
}
