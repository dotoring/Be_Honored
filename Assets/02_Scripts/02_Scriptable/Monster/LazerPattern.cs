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

	private void OnEnable()
	{
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
			}
			else
			{
				syl.SetActive(false);
				this.GetComponent<BoxCollider>().enabled = false;
				float curTime = 0;
				float goalTime = 1.1f;
				if (curTime < goalTime)
				{
					// 경과 시간 업데이트
					curTime += Time.deltaTime * 3;

					
					// 0 ~ 1 사이 비율 계산
					
						float progress = curTime / goalTime;

						// 선형 보간(Lerp)을 통해 벡터 변경
						transform.root.transform.forward = Vector3.Lerp(
						transform.root.transform.forward,
						(bossMonster.targetPlayer.transform.position - transform.root.transform.position).normalized, progress);
					
				}
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
