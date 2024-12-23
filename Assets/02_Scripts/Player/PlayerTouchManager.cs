using UnityEngine;
using System.Collections.Generic;

public class PlayerTouchManager : MonoBehaviour
{
	public List<GameObject> triggerAreas;
	public Material matgray;
	public Material matred;

	private int currentOrder = 0;
	private float lastTriggerTime = 0f;

	private void Start()
	{
		// 시작 시 모든 트리거 영역의 재질을 matgray로 설정 (추가)
		ResetMaterials();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (triggerAreas.Contains(other.gameObject))
		{
			int enteredIndex = triggerAreas.IndexOf(other.gameObject);

			if (enteredIndex == currentOrder)
			{
				if (Time.time - lastTriggerTime <= 1f || currentOrder == 0)
				{
					Debug.Log(other.gameObject.name + " 영역 진입!");

					// 터치된 영역의 재질을 matred로 변경 (추가)
					other.GetComponent<Renderer>().material = matred;

					currentOrder++;
					lastTriggerTime = Time.time;

					if (currentOrder >= triggerAreas.Count)
					{
						Debug.Log("성공!");
						ResetGame(); // 게임 리셋 함수 호출
					}
				}
				else
				{
					Debug.Log("시간 초과! 다시 시도하세요.");
					ResetGame(); // 게임 리셋 함수 호출
				}
			}
			else
			{
				Debug.Log("잘못된 순서입니다.");
				ResetGame(); // 게임 리셋 함수 호출
			}
		}
	}

	// 게임 리셋 함수 (추가)
	private void ResetGame()
	{
		currentOrder = 0;
		ResetMaterials();
	}

	// 모든 트리거 영역의 재질을 matgray로 되돌리는 함수 (추가)
	private void ResetMaterials()
	{
		foreach (GameObject area in triggerAreas)
		{
			area.GetComponent<Renderer>().material = matgray;
		}
	}
}
