using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using ExitGames.Client.Photon;

public enum RoomLevel
{
	One,Two,Three
}

public class PhotonManager : MonoBehaviourPunCallbacks
{
	[SerializeField]private string version;
	[SerializeField]private string nickName;

	[SerializeField] private List<Button> roomInBtns;

	Hashtable customRoomOption = new Hashtable();
	private void Awake()
	{
		for(int i=0;i<roomInBtns.Count;)
			roomInBtns[i].interactable = false;
		

		PhotonNetwork.GameVersion = version;
		PhotonNetwork.NickName = nickName;

		PhotonNetwork.AutomaticallySyncScene = true;

		if (!PhotonNetwork.IsConnected)
			PhotonNetwork.ConnectUsingSettings();
	}

	private void Start()
	{
		for(int i=0;i<roomInBtns.Count;i++)
			roomInBtns[i].onClick.AddListener(()=>MakeRoom((RoomLevel)i));
		customRoomOption.Add("Level", RoomLevel.One);
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
		//print(roomOption.CustomRoomProperties["Level"]);
		PhotonNetwork.CreateRoom("Lv"+roomOption.CustomRoomProperties["Level"], roomOption);
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (var room in roomList)
		{

		}
	}



	public override void OnConnectedToMaster()
	{
		print("서버연결");
		PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		print("로비입장");
		for (int i = 0; i < roomInBtns.Count;)
			roomInBtns[i].interactable = true;
	}

	public override void OnJoinedRoom()
	{
		SceneManager.LoadScene(PhotonNetwork.CurrentRoom.Name);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		print($"방입장 실패 : {returnCode} : {message}");
	}
}
