using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomData : MonoBehaviour
{
	[SerializeField] private TMP_Text roomName;

	private RoomInfo roomInfo;
	public RoomInfo RoomInfo
	{
		get { return roomInfo; }
		set
		{ 
			roomInfo = value;
			roomName.text = roomInfo.Name;
			//버튼 이벤트 연결
			GetComponent<Button>().onClick.AddListener(() =>
			{
				PhotonNetwork.JoinRoom(roomInfo.Name);
			});
		}
	}
}
