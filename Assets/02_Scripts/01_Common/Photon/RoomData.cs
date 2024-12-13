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
			if(roomInfo != null)
				roomName.text = roomInfo.Name + "\n" + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;
			else
				roomName.text = roomNameBase + "\n0 / 0";
		}
	}

	private void Update()
	{
		isRoomInfoNull = roomInfo == null;
	}
}
