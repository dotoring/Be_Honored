using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	public GameObject itemPrefab;  // 아이템 프리팹
	public float itemSpacing = 0.2f; // 아이템 간의 간격

	private List<GameObject> items = new(); // 아이템을 저장할 리스트

	void Start()
	{
		// 초기 아이템 배치
		ArrangeItems();
	}

	// 아이템을 추가하는 함수
	public void AddItem(GameObject item)
	{

		item.transform.SetParent(transform); // 이 스크립트가 붙어있는 오브젝트의 자식으로 설정
		items.Add(item); // 아이템 리스트에 추가
		ArrangeItems(); // 아이템들을 다시 정렬
	}

	// 아이템을 삭제하는 함수
	public void RemoveItem(GameObject item)
	{
		if (items.Contains(item))
		{
			items.Remove(item); // 리스트에서 아이템 제거
			ArrangeItems(); // 아이템들을 다시 정렬
		}
	}

	// 아이템들을 세로로 정렬하는 함수
	private void ArrangeItems()
	{
		for (int i = 0; i < items.Count; i++)
		{
			// 각 아이템의 위치를 세로로 배치
			Vector3 newPosition = new(0, i * itemSpacing, 0);
			items[i].transform.localPosition = newPosition; // Y축을 기준으로 배치
			items[i].transform.localRotation = Quaternion.identity;
		}
	}
}
