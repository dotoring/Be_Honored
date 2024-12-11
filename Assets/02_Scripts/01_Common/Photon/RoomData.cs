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

	public RoomInfo RoomInfo
	{
		get { return roomInfo; }
		set
		{
			roomInfo = value;
			roomName.text = roomInfo.Name + "\n" + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;
		}

	}

	private void Update()
	{
		isRoomInfoNull = roomInfo == null;
	}
}
