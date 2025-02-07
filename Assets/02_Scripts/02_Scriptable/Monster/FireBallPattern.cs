using System.Collections.Generic;
using UnityEngine;

public class FireBallPattern : BossPattern
{
	[SerializeField] private GameObject FirePoint;
	[SerializeField] private List<Transform> FireStartPoint;
	[SerializeField] private List<GameObject> FirePointList;
	[SerializeField] private GameObject FireBallObj;
	[SerializeField] private List<GameObject> targetList=new();

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
		GameObject remove = null;
		if (targetList.Count > 1)//플레이어가 2인 이상일 때
		{
			foreach (var item in bossMonster.playerList)
			{
				float dis = Vector3.SqrMagnitude(
					item.transform.position - this.transform.root.transform.position);
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
			Vector3 temp = item.transform.position;
			temp.y = bossMonster.transform.position.y;
			GameObject Point = Instantiate(FirePoint, temp, Quaternion.identity);
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
			GameObject ball = Instantiate(FireBallObj, FireStartPoint[i].transform.position, Quaternion.identity);
			ball.GetComponent<FireBall>().InitBall(bossMonster.attackPower, FirePointList[i], FireStartPoint[i].transform);
		}
		FirePointList.Clear();
	}
}
