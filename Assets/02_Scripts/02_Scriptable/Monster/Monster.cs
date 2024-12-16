using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public MonsterSpawner spowner;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;

	public Action dieEvent;

	private void Start()
	{
		dieEvent += Test;
		dieEvent += () => spowner.RemoveFromList(this.gameObject);
	}


	private void Test()
	{
		Debug.Log("죽었다!");
	}
}
