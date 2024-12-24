using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System;

public class ExitMgr : MonoBehaviourPunCallbacks
{
	public static Action OnExitDungeon;

	public void ExitDungeon()
	{
		OnExitDungeon();
		PhotonNetwork.LeaveRoom();
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("lobbySample_Working1");
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		Debug.Log("test");
	}

	private void OnDestroy()
	{
		OnExitDungeon = null;
	}
}
