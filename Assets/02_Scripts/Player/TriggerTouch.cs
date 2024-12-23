using UnityEngine;

public class TriggerTouch : MonoBehaviour
{
	public int triggerIndex; // 영역의 순서 (A: 0, B: 1, C: 2)
	public static int triggerOrder = 0;
	public static float lastTriggerTime = 0f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) // 플레이어 태그를 가진 오브젝트만 인식
		{
			if (triggerIndex == triggerOrder)
			{
				if (Time.time - lastTriggerTime <= 1f || triggerOrder == 0) // 첫 트리거 또는 1초 이내 트리거
				{
					Debug.Log(gameObject.name + " 영역 진입!");
					triggerOrder++;
					lastTriggerTime = Time.time;

					if (triggerOrder > 2) // 모든 영역에 진입했을 경우
					{
						Debug.Log("성공!");
						triggerOrder = 0; // 순서 초기화
					}
				}
				else
				{
					Debug.Log("시간 초과! 다시 시도하세요.");
					triggerOrder = 0; // 순서 초기화
				}
			}
			else
			{
				Debug.Log("잘못된 순서입니다.");
				triggerOrder = 0; // 순서 초기화
			}
		}
	}
}
