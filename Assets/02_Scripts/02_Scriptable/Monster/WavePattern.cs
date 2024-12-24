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
	//각도 0 90 180 270
	private void OnEnable()
	{
		curTime = 0;
		transform.eulerAngles = Random.Range(0, 4) * 90.0f * Vector3.up;
		PatternOne(Random.Range(0, startPoints.Count));
	}
	private void Update()
	{
		if (curTime < waitTime)
			curTime += Time.deltaTime;
		else
		{
			foreach (var ball in currentBalls)
			{
				ball.transform.position += ball.transform.forward * Time.deltaTime * speed;
			}
		}
	}
	private void OnDisable()
	{
		foreach(var ball in currentBalls)
		{
			Destroy(ball);
		}
		currentBalls.Clear();
	}
	//공 갯수 > 5개?

	//패턴
	//1. 공 갯수 중에 한군데 빵꾸
	//2. 0 2 0 4 0 공 나오게
	//3. 1 0 3 0 5 공 나오게

	void PatternOne(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if(i!=num)
			{
				GameObject ball = Instantiate(ironBall, startPoints[i]);
				currentBalls.Add(ball);
			}	
		}
	}
	void PatternTwo(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if (i%2==num)
			{
				GameObject ball = Instantiate(ironBall, startPoints[i]);
				currentBalls.Add(ball);
			}
		}
	}

}
