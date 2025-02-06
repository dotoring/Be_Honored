using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Burst.Intrinsics;
using System;

public class PlayerTouchManager : MonoBehaviour
{
	public List<GameObject> triggerAreas;
	[SerializeField] List<Vector3> positionA;
	[SerializeField] List<Vector3> positionB;
	[SerializeField] List<Vector3> rotationA;
	[SerializeField] List<Vector3> rotationB;

	bool IsposiA;
	public Material matgray;  // 회색
	public Material matred;   // 빨강
	public Material matblue;  // 파랑

	protected int currentOrder = 0;
	private float lastTriggerTime = 0f;
	[SerializeField] private protected AudioClip attacksound;
	private protected AudioSource audioSource;

	protected Vector3 offset = new(0, 0, 1);
	protected Vector3 sizeOfBox = new(1, 1, 1.5f);
	public LayerMask m_LayerMask;
	bool hited = false;

	PhotonView pv;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		pv = GetComponent<PhotonView>();
	}

	private void Start()
	{
		if (!pv.IsMine) return;
		triggerAreas = Player.Instance.Attackon;
		// 게임 시작 시 currentOrder에 맞는 공만 파랑으로 설정
		SetAreaToColors(currentOrder);
		SetPositionChange();
	}

	private void Update()
	{
		if (Player.Instance.IsArm)
		{

			// 타임아웃 처리: 현재 공을 터치했으면, 다음 공을 1초 내에 터치하지 않으면 리셋
			if (currentOrder > 0 && Time.time - lastTriggerTime > 1f)
			{
				// 타임아웃되었으면 리셋
				ResetGame();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!Player.Instance.IsArm) return;
		if (triggerAreas.Contains(other.gameObject))
		{
			int enteredIndex = triggerAreas.IndexOf(other.gameObject);

			// 현재 순서와 일치하는 영역인지 확인
			if (enteredIndex == currentOrder)
			{
				// 터치된 공이 currentOrder 공이라면
				Debug.Log(other.gameObject.name + " 영역 진입!");

				// 터치 시간을 기록
				lastTriggerTime = Time.time;

				// 현재 순서 증가
				currentOrder++;

				// 다음 순서의 영역을 matblue로 설정
				if (currentOrder < triggerAreas.Count)
				{
					SetAreaToColors(currentOrder);
				}

				// 모든 영역을 터치했다면 공격 및 리셋
				if (currentOrder >= triggerAreas.Count)
				{
					Debug.Log("성공!");
					SetPositionChange();
					Attack();
					ResetGame(); // 게임 리셋 함수 호출
				}
			}
			else if (enteredIndex == (currentOrder - 1))
			{
				return;
			}
			else
			{
				Debug.Log("잘못된 순서입니다.");
				ResetGame(); // 게임 리셋 함수 호출
			}
		}
	}

	protected void SetPositionChange()
	{
		if (IsposiA)
		{
			for (int i = 0; i < triggerAreas.Count; i++)
			{
				triggerAreas[i].transform.localPosition = positionB[i];
				triggerAreas[i].transform.localRotation = Quaternion.Euler(rotationB[i]);

			}
			IsposiA = !IsposiA;
		}
		else
		{
			for (int i = 0; i < triggerAreas.Count; i++)
			{
				triggerAreas[i].transform.localPosition = positionA[i];
				triggerAreas[i].transform.localRotation = Quaternion.Euler(rotationA[i]);
			}
			IsposiA = !IsposiA;
		}
	}

	// 게임 리셋 함수
	private void ResetGame()
	{
		currentOrder = 0;
		// 현재 상태를 파랑 회색 회색으로 리셋
		SetAreaToColors(currentOrder);
	}

	// 각 공의 재질을 설정 (파랑, 회색, 회색)
	protected void SetAreaToColors(int order)
	{
		// 모든 영역을 matgray로 리셋
		foreach (GameObject area in triggerAreas)
		{
			foreach (var VARIABLE in area.GetComponentsInChildren<Renderer>())
			{
				VARIABLE.material = matgray;
			}


			//area.GetComponentsInChildren<Renderer>().material = matgray;
		}

		// currentOrder에 해당하는 공을 matblue로 설정
		if (order < triggerAreas.Count)
		{
			foreach (var VARIABLE in triggerAreas[order].GetComponentsInChildren<Renderer>())
			{
				VARIABLE.material = matblue;
			}
			//triggerAreas[order].GetComponent<Renderer>().material = matblue;
		}

	}

	protected virtual void Attack()
	{
		if (!Player.Instance.IsArm) return;
		//audioSource.PlayOneShot(attacksound);  // Todo Test Code for attack
		hited = false;
		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y, sizeOfBox / 2, Quaternion.identity, m_LayerMask);

		foreach (var item in hitColliders)
		{
			item.GetComponent<PhotonView>().RPC("Damaged", RpcTarget.AllBuffered, Player.Instance._stat.attack);
			hited = true;
		}

		if (hited) audioSource.PlayOneShot(attacksound);
	}
}
