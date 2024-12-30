using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class LazerPattern : MonoBehaviour
{
	[SerializeField] private BossMonster bossMonster;
	[SerializeField] private float rotateAngle;
	[SerializeField] private GameObject range;
	[SerializeField] private GameObject syl;

	[SerializeField] private float chargingTime;
	[SerializeField] private float lazingTime;

	[SerializeField] float returnTime;
	[SerializeField] float goalTime = 1.1f;
	[SerializeField] float progress;
	[SerializeField] Vector3 vec;

	private void OnEnable()
	{
		returnTime = 0;
		chargingTime = 0;
		lazingTime = 0;
		range.SetActive(true);
	}


	private void Update()
	{
		if (chargingTime < 2.0f)
		{
			chargingTime += Time.deltaTime;
			transform.position += Vector3.up*2*Time.deltaTime;
		}
		else
		{
			range.SetActive(false);
			syl.SetActive(true);
			if (lazingTime < 2.0f)
			{
				lazingTime += Time.deltaTime;
				transform.root.transform.eulerAngles += rotateAngle * Time.deltaTime * Vector3.up;
				vec = transform.root.transform.forward;//종료당시 전방 벡터를 저장함
			}
			else
			{
				syl.SetActive(false);
				this.GetComponent<BoxCollider>().enabled = false;
				if (returnTime < goalTime)
				{

					// 경과 시간 업데이트
					returnTime += Time.deltaTime;

		
					// 0 ~ 1 사이 비율 계산
					
					progress = returnTime / goalTime;

						// 선형 보간(Lerp)을 통해 벡터 변경
					transform.root.transform.forward = Vector3.Lerp(
					vec,
					(bossMonster.targetPlayer.transform.position-bossMonster.transform.position).normalized, progress);
					
				}
			    else
					gameObject.SetActive(false);
			}
		}
	}


	public void InitThis(BossMonster bossmonster, float angle)
	{
		bossMonster = bossmonster;
		rotateAngle = angle;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (chargingTime < 2.0f)
			{
				print("경고");
			}
			else
			{
				print("데미지");
			}
		}
	}
}
