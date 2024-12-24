using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WavePattern : MonoBehaviour
{
	public BossMonster bossMonster;
	[SerializeField] private GameObject ironBall;
	[SerializeField] private List<Transform> startPoints;
	[SerializeField] private List<GameObject> currentBalls;
	[SerializeField] private float speed = 3.0f;

	[SerializeField] private float waitTime=1.0f;
	[SerializeField] private float curTime=0.0f;
	[SerializeField] private float cur2Time=0.0f;
	//각도 0 90 180 270
	private void OnEnable()
	{
		curTime = 0;
		cur2Time = 0;
		transform.eulerAngles = Random.Range(0, 4) * 90.0f * Vector3.up;
		int i=Random.Range(0, 2);
		if (i == 0)
			DoPatternOne(Random.Range(0, startPoints.Count));
		else
			DoPatternTwo(Random.Range(0, 2));
	}
	private void Update()
	{
		if (curTime < waitTime)
			curTime += Time.deltaTime;
		else
		{
			cur2Time += Time.deltaTime;
			foreach (var ball in currentBalls)
			{
				ball.transform.position += ball.transform.forward * Time.deltaTime * speed;
				if(ball.transform.localPosition.z>=8)
				{
					gameObject.SetActive(false);
					break;
				}
			}
		}
	}
	private void OnDisable()
	{
		foreach(var ball in currentBalls)
		{
			Destroy(ball.gameObject);
		}
		currentBalls.Clear();
	}

	void DoPatternOne(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if(i!=num)
			{
				MakeBall(startPoints[i]);
			}	
		}
	}
	void DoPatternTwo(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if (i%2==num)
			{
				MakeBall(startPoints[i]);
			}
		}
	}

	void MakeBall(Transform point)
	{
		GameObject ball = Instantiate(ironBall, point);
		currentBalls.Add(ball);
		ball.GetComponent<IronBall>().InitBall(bossMonster.attackPower);
	}
}
