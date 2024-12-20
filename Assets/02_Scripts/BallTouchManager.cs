using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class BallTouchManager : MonoBehaviour
{
	public GameObject ballA;
	public GameObject ballB;
	public GameObject ballC;

	public Material matRed; // 터치된 후 바꿀 마테리얼
	public Material matGray; // 기본 마테리얼

	private int touchOrder = 0; // 현재 터치 순서를 추적 (0 = 아직 시작 안함, 1 = A, 2 = B, 3 = C)
	private float touchTimeout = 1f; // 터치 간의 유효 시간 (1초)
	private bool isProcessing = false; // 비동기 작업 중인지 확인하는 플래그

	Vector3 offset = new(0, 0, 1);
	Vector3 sizeOfBox = new(1, 1, 1.5f);
	public LayerMask m_LayerMask;
	[SerializeField] AudioClip attacksound;
	AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private async void OnTriggerEnter(Collider other)
	{
		//Debug.Log($"other name {other.name}");
		// if (isProcessing)
		// 	return; // 현재 비동기 작업 중이라면 무시

		// 터치된 공이 순서대로 올 때만 처리
		if (other.CompareTag("Ball"))
		{
			//Debug.Log($"touch object : {other.gameObject.name}");
			if (other.gameObject == ballA && touchOrder == 0)
			{
				//		Debug.Log($"Ball 1 touch");
				await HandleBallTouch(ballA, 1); // A 공 터치
			}
			else if (other.gameObject == ballB && touchOrder == 1)
			{
				//		Debug.Log($"Ball 2 touch");
				await HandleBallTouch(ballB, 2); // B 공 터치
			}
			else if (other.gameObject == ballC && touchOrder == 2)
			{
				//		Debug.Log($"Ball 3 touch");
				await HandleBallTouch(ballC, 3); // C 공 터치
			}
		}
	}

	private async Task HandleBallTouch(GameObject ball, int nextOrder)
	{
		// 비동기 처리 중임을 나타내는 플래그 설정
		isProcessing = true;

		// 마테리얼 변경
		ball.GetComponent<Renderer>().material = matRed;
		touchOrder = nextOrder;

		// 로그 출력
		//Debug.Log($"{ball.name} touched!");

		// 1초 동안 기다리고, 그 사이에 다음 공이 터치되었는지 확인
		bool timeoutOccurred = await WaitForNextTouch();

		// 유효 기간 내에 다음 공을 터치하지 않으면 리셋
		if (timeoutOccurred)
		{
			//Debug.Log("Next ball not touched in time. Resetting...");
			ResetBallMaterials();
			touchOrder = 0; // 순서 리셋
		}

		// 비동기 처리 완료 후 플래그 리셋
		isProcessing = false;
	}

	private async Task<bool> WaitForNextTouch()
	{
		float timer = 0f;
		while (timer < touchTimeout)
		{
			if (touchOrder == 3)
			{
				// 마지막 공까지 터치되면 종료
				//Debug.Log("All balls touched in order!");
				touchOrder = 0;
				Attack();
				return false; // 리셋 필요 없음
			}

			timer += Time.deltaTime;
			await Task.Yield(); // 비동기 대기

		}

		return true; // 유효 시간 내에 다음 공이 터치되지 않으면 true 반환
	}

	private void Attack()
	{

		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y, sizeOfBox / 2, Quaternion.identity, m_LayerMask);

		// int i = 0;
		// //Check when there is a new collider coming into contact with the box
		// while (i < hitColliders.Length)
		// {
		// 	//Output all of the collider names
		// 	Debug.Log("Hit : " + hitColliders[i].name + i);
		// 	//Increase the number of Colliders in the array
		// 	i++;
		// }
		foreach (var item in hitColliders)
		{
			item.GetComponent<Monster>()?.Damaged(1);
		}
		audioSource.PlayOneShot(attacksound);
		ResetBallMaterials();
	}

	private void ResetBallMaterials()
	{
		// 모든 공의 마테리얼을 기본으로 리셋
		ballA.GetComponent<Renderer>().material = matGray;
		ballB.GetComponent<Renderer>().material = matGray;
		ballC.GetComponent<Renderer>().material = matGray;
	}
}
