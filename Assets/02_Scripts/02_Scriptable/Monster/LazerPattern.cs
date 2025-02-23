using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class LazerPattern : BossPattern
{
	[SerializeField] private float rotateAngle = 30;
	[SerializeField] private GameObject range;
	[SerializeField] private GameObject syl;
	[SerializeField] private Collider col;

	[SerializeField] private float chargingTime;
	[SerializeField] private float lazingTime;

	[SerializeField] float returnTime;
	[SerializeField] float goalTime = 1.1f;
	[SerializeField] float progress;
	[SerializeField] Vector3 vec;
	[SerializeField] bool startShoot;

	private void OnEnable()
	{
		transform.localPosition = Vector3.up*-0.4f;
		returnTime = 0;
		chargingTime = 0;
		lazingTime = 0;
		col.enabled = false;
		startShoot = false;
		range.SetActive(true);
	}


	private void Update()
	{
		if (chargingTime < 2.0f)
		{
			chargingTime += Time.deltaTime;
			transform.position += Vector3.up*0.5f*Time.deltaTime;
		}
		else
		{
			if (!startShoot)
			{
				col.enabled = true;
				range.SetActive(false);
				if (!syl.activeSelf)
					syl.SetActive(true);
				startShoot= true;
			}
			if (lazingTime < 5.0f)
			{
				lazingTime += Time.deltaTime;
				transform.root.transform.eulerAngles += rotateAngle * Time.deltaTime * Vector3.up;
				vec = transform.root.transform.forward;//종료당시 전방 벡터를 저장함
			}
			else
			{
				syl.SetActive(false);
				col.enabled = false;
				if (returnTime < goalTime)
				{

					// 경과 시간 업데이트
					returnTime += Time.deltaTime;

		
					// 0 ~ 1 사이 비율 계산
					
					progress = returnTime / goalTime;

					Vector3 temp=(bossMonster.targetPlayer.transform.position - bossMonster.transform.position).normalized;
					temp.y = 0;

						// 선형 보간(Lerp)을 통해 벡터 변경
					transform.root.transform.forward = Vector3.Lerp(
					vec,
					temp, progress);
					
				}
			    else
					gameObject.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")&&PhotonNetwork.IsMasterClient)
		{
			other.GetComponent<HitPlayer>()?.Damaged(bossMonster.attackPower);
		}
	}
}
