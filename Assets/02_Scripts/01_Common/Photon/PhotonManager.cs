using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using System.Collections;

public enum RoomLevel
{
	One, Two, Three
}

public class PhotonManager : MonoBehaviourPunCallbacks
{
	[SerializeField] private string version;
	[SerializeField] private string nickName;

	[SerializeField] private List<Button> roomInBtns;
	ExitGames.Client.Photon.Hashtable customRoomOption = new();
	[SerializeField] Image LoadingBar;
	[SerializeField] GameObject canvas;


	private void Awake()
	{
		print("pm Awake");
		if (!PhotonNetwork.IsConnected)
		{
			print("pn 연결 없음");
			for (int i = 0; i < roomInBtns.Count; i++)
				roomInBtns[i].interactable = false;


			PhotonNetwork.GameVersion = version;
			PhotonNetwork.NickName = nickName;

			PhotonNetwork.AutomaticallySyncScene = true;

			PhotonNetwork.ConnectUsingSettings();
		}

	}

	private void Start()
	{
		print("PM Start : 버튼 이벤트 설정");
		roomInBtns[0].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.One));
		roomInBtns[1].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Two));
		roomInBtns[2].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Three));
		customRoomOption.Add("Level", RoomLevel.One);
		// if (PhotonNetwork.IsConnected)
		// 	PhotonNetwork.JoinLobby();
	}

	public void MakeRoomBtnOnClick(RoomLevel level)
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
		PhotonNetwork.CreateRoom("Lv" + roomOption.CustomRoomProperties["Level"], roomOption);
	}

	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		print("방 리스트 변경 콜백");
		foreach (var roomdata in roomInBtns)
		{
			bool ismatch = false;
			foreach (var room in roomList)
			{
				print("방 리스트 이름 = " + room.Name);
				if (roomdata.gameObject.name.Contains(room.Name))
				{
					ismatch = true;
					roomdata.GetComponent<RoomData>().RoomInfo = room;
				}
			}
			if (ismatch == false)
			{
				roomdata.GetComponent<RoomData>().RoomInfo = null;
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
		for (int i = 0; i < roomInBtns.Count; i++)
		{
			roomInBtns[i].interactable = true;
		}
	}

	public override void OnJoinedRoom()
	{
		Player.Instance.SavePlayerData();
		print("방입장 :" + PhotonNetwork.CurrentRoom.Name);
		StartCoroutine(EnterRoomAsync());
	}

	IEnumerator EnterRoomAsync()
	{
		canvas.SetActive(true);
		Player.Instance.ChangeBGM(1);
		yield return null;
		AsyncOperation aload = SceneManager.LoadSceneAsync(PhotonNetwork.CurrentRoom.Name);
		while (!aload.isDone)
		{
			LoadingBar.fillAmount = aload.progress;
			yield return null;
		}

	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		print($"방입장 실패 : {returnCode} : {message}");
	}

	public override void OnDisconnected(DisconnectCause info)
	{
		Player.Instance.SavePlayerData();
	}
}
