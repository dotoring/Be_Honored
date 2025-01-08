using System;
using System.Collections.Generic;
using UnityEngine;

public class StonePattern : MonoBehaviour
{
	[SerializeField] private BossMonster boss;
	[SerializeField] private List<Transform> point;
	[SerializeField] private List<GameObject> stones;
	[SerializeField] private List<float> waitTimes;
	[SerializeField] private float height;
	[SerializeField] private float speed;
	[SerializeField] private float count;
	public Action endPattern;

	private void Start()
	{
		endPattern += () =>
		{
			count++;
			if (count >= stones.Count)
			{
				this.gameObject.SetActive(false);
			}
		};
	}
	private void OnEnable()
	{
		count = 0;
		for(int i=0;i<point.Count; i++)
		{
			point[i].localPosition =
				new Vector3(UnityEngine.Random.Range(-30, 30) / 10.0f, 0, UnityEngine.Random.Range(-30, 30) / 10.0f);
			stones[i].SetActive(true);
			stones[i].transform.localPosition = Vector3.up * height;
			stones[i].GetComponent<Stone>().InitStone(this, point[i],
				waitTimes[UnityEngine.Random.Range(0,waitTimes.Count)], boss.attackPower * 2f,speed);
		}
	}
}
