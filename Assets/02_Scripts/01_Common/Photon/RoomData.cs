using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomData : MonoBehaviour
{
	[SerializeField] private TMP_Text roomName;
	[SerializeField] private string roomNameBase;
	private RoomInfo roomInfo;
	[SerializeField] private bool isRoomInfoNull;
	[SerializeField] public RoomLevel roomLevel;


	private void Awake()
	{
		print("버튼 aw : " + name);
	}

	private void Start()
	{
		print("버튼 on : "+name);
		PhotonManager.Instance.roomInBtns.Add(this.GetComponent<Button>());
		PhotonManager.Instance.InCount++;
		RoomInfo = RoomInfo;
	}

	public RoomInfo RoomInfo
	{
		get { return roomInfo; }
		set
		{
			roomInfo = value;
			if(roomInfo != null)
				roomName.text = roomInfo.Name + "\n" + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;
			else
				roomName.text = roomNameBase + "\n0 / 0";
		}
	}

	private void Update()
	{
		isRoomInfoNull = (roomInfo == null);
		//print(gameObject.name + "방 정보 있냐? : "+isRoomInfoNull);
	}
}
