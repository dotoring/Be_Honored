using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class WavePattern : MonoBehaviour
{
	public BossMonster bossMonster;
	[SerializeField] private GameObject ironBall;
	[SerializeField] private List<Transform> startPoints;
	[SerializeField] private List<Transform> endPoints;
	[SerializeField] private List<GameObject> ironBalls;
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
			foreach (var ball in ironBalls)
			{
				if(ball==null)
					continue;
				ball.transform.position += ball.transform.forward * Time.deltaTime * speed;
				float dis = Vector3.Distance(ball.transform.position, endPoints[i].position);
				if (dis <= 0.01)
				{
					gameObject.SetActive(false);
					break;
				}
				i++;
			}
		}
	}

	void DoPatternOne(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if(i!=num)
			{
				ActiveBall(i);
			}
			else
			{
				ironBalls[i].gameObject.SetActive(false);
			}
		}
	}
	void DoPatternTwo(int num)
	{
		for (int i = 0; i < startPoints.Count; i++)
		{
			if (i%2==num)
			{
				ActiveBall(i);
			}
			else
			{
				ironBalls[i].gameObject.SetActive(false);
			}
		}
	}

	void ActiveBall(int num)
	{
		GameObject ball = ironBalls[num].gameObject;
		ball.SetActive(true);
		ball.transform.position = startPoints[num].position;
		ball.transform.rotation = startPoints[num].rotation;
	}
}
