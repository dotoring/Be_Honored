using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonTest : MonoBehaviourPunCallbacks
{
	[SerializeField] private string version;
	[SerializeField] private string nickName;

	[SerializeField] private Button roomInBtn;
	ExitGames.Client.Photon.Hashtable customRoomOption = new();
	[SerializeField] Image LoadingBar;
	[SerializeField] GameObject canvas;


	private void Awake()
	{
		print("pm Awake");
		if (!PhotonNetwork.IsConnected)
		{
			print("pn 연결 없음");
			roomInBtn.interactable = false;


			PhotonNetwork.GameVersion = version;
			PhotonNetwork.NickName = nickName;

			PhotonNetwork.AutomaticallySyncScene = true;

			PhotonNetwork.ConnectUsingSettings();
		}

	}

	private void Start()
	{
		print("PM Start : 버튼 이벤트 설정");
		roomInBtn.onClick.AddListener(() => PhotonNetwork.JoinRandomRoom());
	}

	private void MakeRoom(RoomLevel roomLevel)
	{
		customRoomOption["Level"] = roomLevel;
		var roomOption = new RoomOptions
		{
			MaxPlayers = 4,
			IsOpen = true,
			IsVisible = true,
			CustomRoomProperties = customRoomOption
		};
		print(roomOption.CustomRoomProperties["Level"]);
		PhotonNetwork.CreateRoom("Lv" + roomOption.CustomRoomProperties["Level"], roomOption);
	}

	public override void OnConnectedToMaster()
	{
		print("서버연결");
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		print("로비입장");
		roomInBtn.interactable = true;
	}

	public override void OnJoinedRoom()
	{
		//Player.Instance.SavePlayerData();
		print("방입장 :" + PhotonNetwork.CurrentRoom.Name);
		SceneManager.LoadScene("MonsterTestScene");
	}
	

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		print($"방입장 실패 : {returnCode} : {message}");
		PhotonNetwork.CreateRoom("TestRoom", new RoomOptions { MaxPlayers = 2 });
	}

	public override void OnDisconnected(DisconnectCause info)
	{
		Player.Instance.SavePlayerData();
	}
}
