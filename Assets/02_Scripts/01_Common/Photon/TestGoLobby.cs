using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestGoLobby : MonoBehaviourPunCallbacks
{
	[SerializeField] private Button LobbyBtn;

	private void Start()
	{
		LobbyBtn.onClick.AddListener(()=>PhotonNetwork.LeaveRoom());
	}
	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("LobbyScene");
	}
}
