using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using NUnit.Framework.Interfaces;

public class RoomData : MonoBehaviour
{
	[SerializeField] private TMP_Text roomName;
	[SerializeField] private string roomNameBase;
	private RoomInfo roomInfo;
	[SerializeField] private bool isRoomInfoNull;
	[SerializeField] public RoomLevel roomLevel;

	[SerializeField] private Button btn;
	[SerializeField] public int cost;

	public Action<int> btnActivate;
	private void Awake()
	{
		print("버튼 aw : " + name);
		btnActivate += (value) =>
		{
			if (value >= cost&&btn.interactable==false)
			{
				btn.interactable = true;
				RoomInfo = RoomInfo;
			}
			else if(value < cost && btn.interactable==true)
			{
				btn.interactable=false;
				RoomInfo = RoomInfo;
			}
		};
	}

	private void Start()
	{
		print("버튼 on : "+name);
		PhotonManager.Instance.roomInBtns.Add(btn);
		PhotonManager.Instance.InCount++;
		RoomInfo = RoomInfo;
	}

	public RoomInfo RoomInfo
	{
		get { return roomInfo; }
		set
		{
			roomInfo = value;
			if (roomInfo != null)
			{
				roomName.text = "Lv." + ((int)roomLevel + 1) + "\n" + roomInfo.PlayerCount + " / " + roomInfo.MaxPlayers;
			}
			else if(btn.interactable==false)
			{
				roomName.text = "Lv." + ((int)roomLevel + 1) + "\n" + "Cost : "+cost;
			}
			else
			{
				roomName.text = roomNameBase + "\n0 / 0";
			}
		}
	}

	private void Update()
	{
		isRoomInfoNull = (roomInfo == null);
		//print(gameObject.name + "방 정보 있냐? : "+isRoomInfoNull);
	}
}
