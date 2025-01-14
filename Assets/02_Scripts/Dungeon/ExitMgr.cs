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

	public void ExitDungeon()
	{
		OnExitDungeon?.Invoke();
		PhotonNetwork.LeaveRoom();
	}

	

	public void TeleportToUnderStage()
	{
		player.transform.position = underStage.position;
	}

	private void OnDestroy()
	{
		OnExitDungeon = null;
	}
}
