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
	private static PhotonManager instance;

	[SerializeField] private string version;
	[SerializeField] private string nickName;

	[SerializeField] private int invokeCount;
	//invokeCount로 버튼에 3을 호출시켜서 3에서 버튼이벤트 및 룸 리스트 갱신 해주기
	public int InCount
	{
		get { return invokeCount; }
		set
		{
			invokeCount= value;
			if(invokeCount>=3)
			{
				roomInBtns.Sort((Button a,Button b) => { return a.GetComponent<RoomData>().roomLevel.CompareTo(b.GetComponent<RoomData>().roomLevel); });
				roomInBtns[0].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.One));
				roomInBtns[1].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Two));
				roomInBtns[2].onClick.AddListener(() => MakeRoomBtnOnClick(RoomLevel.Three));
				if(PhotonNetwork.IsConnected)
					PhotonNetwork.JoinLobby();
				for (int i = 0; i < roomInBtns.Count; i++)
				{
					roomInBtns[i].GetComponent<RoomData>().btnActivate.Invoke(App.Instance.gold.Value);
				}
			}
		}
	}
	[SerializeField] public List<Button> roomInBtns;
	ExitGames.Client.Photon.Hashtable customRoomOption = new();
	//[SerializeField] Image LoadingBar;
	//[SerializeField] GameObject canvas;

	public static PhotonManager Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject singletonObject = new GameObject(typeof(PhotonManager).Name);
				instance = singletonObject.AddComponent<PhotonManager>();
				DontDestroyOnLoad(singletonObject);
			}
			return instance;
		}
	}

	private void Awake()
	{
		if(instance!=null&&instance!=this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);

		instance = this;

		print("pm Awake");
		if (!PhotonNetwork.IsConnected)
		{
			print("pn 연결 없음");

			PhotonNetwork.GameVersion = version;
			PhotonNetwork.NickName = nickName;

			PhotonNetwork.AutomaticallySyncScene = true;

			PhotonNetwork.ConnectUsingSettings();
		}

	}

	private void Start()
	{
		customRoomOption.Add("Level", RoomLevel.One);
	}

	public override void OnEnable()
	{
		PhotonNetwork.AddCallbackTarget(this);
	}

	public void MakeRoomBtnOnClick(RoomLevel level)
	{
		print("들어온 값" + (int)level);
		App.Instance.UseGold(roomInBtns[(int)level].GetComponent<RoomData>().cost);
		if (roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo == null || roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo.MaxPlayers == 0)
			MakeRoom(level);
		else
		{
			print(roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo.Name);
			PhotonNetwork.JoinRoom(roomInBtns[(int)level].GetComponent<RoomData>().RoomInfo.Name);
		}
	}

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);
		print("방입장실패");
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
		print(SceneManager.GetActiveScene().name);
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

	public override void OnLeftRoom()
	{
		StartCoroutine(LeftRoomAsync());
	}

	IEnumerator LeftRoomAsync()
	{

		Debug.Log("Left Room.0");

		yield return null;
		AsyncOperation aload = SceneManager.LoadSceneAsync("lobbySample_Working1");
		//while (!aload.isDone)
		//{
		//	loadingbar.fillAmount = aload.progress;
		//	yield return null;
		//}

	}

	public override void OnConnectedToMaster()
	{
		print("서버연결");
		if(invokeCount>=3)
			PhotonNetwork.JoinLobby();
	}

	public override void OnJoinedLobby()
	{
		print("로비입장");
		Player.Instance.ChangeBGM(0);
	}

	public override void OnJoinedRoom()
	{
		roomInBtns.Clear();
		invokeCount = 0;
		Player.Instance.SavePlayerData();
		print("방입장 :" + PhotonNetwork.CurrentRoom.Name);
		StartCoroutine(EnterRoomAsync());
	}

	IEnumerator EnterRoomAsync()
	{

		Player.Instance.ChangeBGM(1);
		yield return null;
		if (PhotonNetwork.IsMasterClient)
		{
			AsyncOperation aload = SceneManager.LoadSceneAsync(PhotonNetwork.CurrentRoom.Name);
			while (!aload.isDone)
			{
				//LoadingBar.fillAmount = aload.progress;
				yield return null;
			}
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
