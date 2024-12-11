using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using TMPro;

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
		for(int i=0;i<roomInBtns.Count;i++)
			roomInBtns[i].interactable = false;
		

		PhotonNetwork.GameVersion = version;
		PhotonNetwork.NickName = nickName;

		PhotonNetwork.AutomaticallySyncScene = true;

		if (!PhotonNetwork.IsConnected)
			PhotonNetwork.ConnectUsingSettings();
	}

	private void Start()
	{
		roomInBtns[0].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.One));
		roomInBtns[1].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Two));
		roomInBtns[2].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Three));
		customRoomOption.Add("Level", RoomLevel.One);
	}

	void MakeRoomBtnOnClick(RoomLevel level)
	{
		if (roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo == null || roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo.MaxPlayers == 0)
			MakeRoom(level);
		else
			PhotonNetwork.JoinRoom(roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo.Name);
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
		PhotonNetwork.CreateRoom("Lv"+roomOption.CustomRoomProperties["Level"], roomOption);
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (var room in roomList)
		{
			print("방 리스트 이름 = " + room.Name);
			foreach(var roomdata in roomInBtns)
			{
				if (roomdata.gameObject.name.Contains(room.Name))
				{
					roomdata.GetComponent<RoomData>().RoomInfo = room;
				}
			}
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
		for (int i = 0; i < roomInBtns.Count;i++)
			roomInBtns[i].interactable = true;
	}

	public override void OnJoinedRoom()
	{
		print("방입장 :" + PhotonNetwork.CurrentRoom.Name);
		SceneManager.LoadScene(PhotonNetwork.CurrentRoom.Name);
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		print($"방입장 실패 : {returnCode} : {message}");
	}
}
