using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ExitMgr : MonoBehaviourPunCallbacks
{

	public void ExitDungeon()
	{
		PhotonNetwork.LeaveRoom();
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("LobbyScene");
	}
}
