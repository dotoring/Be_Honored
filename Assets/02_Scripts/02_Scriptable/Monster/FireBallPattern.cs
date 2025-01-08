using Photon.Pun.Demo.Asteroids;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Pool;

public class FireBallPattern : BossPattern
{
	[SerializeField] private GameObject FirePoint;
	[SerializeField] private List<Transform> FireStartPoint;
	[SerializeField] private List<GameObject> FirePointList;
	[SerializeField] private GameObject FireBallObj;
	[SerializeField] private List<GameObject> targetList;

	private float curTime;

	private void OnEnable()
	{
		targetList.Clear();
		curTime = 0;
		foreach (var item in bossMonster.playerList)
		{
			targetList.Add(item);
		}
		float mindis = 99.0f;
		GameObject remove = new GameObject();
		if (targetList.Count > 1)//플레이어가 2인 이상일 때
		{
			foreach (var item in bossMonster.playerList)
			{
				float dis = Vector3.Magnitude(item.transform.position - this.transform.root.transform.position);
				if (mindis > dis)
				{
					mindis = dis;
					remove = item;
				}
			}
			targetList.Remove(remove);
		}

		foreach (var item in targetList)
		{
			GameObject Point = GameObject.Instantiate(FirePoint, item.transform.root.position, Quaternion.identity);
			FirePointList.Add(Point);
		}
	}
	private void Update()
	{
		curTime += Time.deltaTime;
		if (curTime > 3.0f)
		{
			ThrowFireBall();
			gameObject.SetActive(false);
		}
	}

	private void ThrowFireBall()
	{
		for (int i = 0; i < FirePointList.Count; i++)
		{
			GameObject ball = (GameObject)GameObject.Instantiate(FireBallObj, FireStartPoint[i].transform.position, Quaternion.identity);
			ball.GetComponent<FireBall>().InitBall(bossMonster, FirePointList[i], FireStartPoint[i].transform);
		}
		FirePointList.Clear();
	}
}
