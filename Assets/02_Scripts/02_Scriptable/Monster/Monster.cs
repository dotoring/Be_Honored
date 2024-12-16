using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
	public MonsterSpawner spawner;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;

	public Action dieEvent;

	private void Start()
	{
		dieEvent += () => spawner.RemoveFromList(this.gameObject);
	}
}
