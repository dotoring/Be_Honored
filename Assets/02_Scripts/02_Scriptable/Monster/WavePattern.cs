using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WavePattern : MonoBehaviour
{
	public BossMonster bossMonster;
	[SerializeField] private GameObject ironBall;
	[SerializeField] private List<Transform> startPoints;
	[SerializeField] private List<Transform> endPoints;
	[SerializeField] private List<GameObject> currentBalls;
	[SerializeField] private float speed = 3.0f;

	[SerializeField] private float waitTime=1.0f;
	[SerializeField] private float curTime=0.0f;

	private void OnEnable()
	{
		curTime = 0;
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
			int i = 0;
			foreach (var ball in currentBalls)
			{
				ball.transform.position += ball.transform.forward * Time.deltaTime * speed;
				//if (ball.transform.position.z >= endPoints[i].position.z)
				//{
				//	gameObject.SetActive(false);
				//	break;
				//}
				i++;
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
				MakeBall(startPoints[i], endPoints[i]);
			}	
		}
	}
	void DoPatternTwo(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if (i%2==num)
			{
				MakeBall(startPoints[i], endPoints[i]);
			}
		}
	}

	void MakeBall(Transform startPoint,Transform endPoint)
	{
		GameObject ball = Instantiate(ironBall, startPoint.position, Quaternion.identity);
		ball.transform.forward = startPoint.forward;
		currentBalls.Add(ball);
		ball.GetComponent<IronBall>().InitBall(bossMonster.attackPower,endPoint.position);
	}
}
