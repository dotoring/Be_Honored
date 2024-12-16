using UnityEngine;
using System.Collections.Generic;
using Unity.Behavior;
using NUnit.Framework;

public class BossMonster : MonoBehaviour
{
	public MonsterSpawner spawner;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;
	public bool isDoorOpen;

	public List<GameObject> playerList;

	public System.Action dieEvent;

	private void Start()
	{
		dieEvent += () => spawner.RemoveFromList(this.gameObject);
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in temp)
		{
			playerList.Add(go);
		}
		
	}
}
