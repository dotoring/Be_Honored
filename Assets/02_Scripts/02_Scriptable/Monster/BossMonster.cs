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
	public float skillCoolTime;
	public float skillWaitTime=0;
	public bool canUseSkill;

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
	private void Update()
	{
		if (isDoorOpen == true)
		{
			skillWaitTime += Time.deltaTime;
			if (skillCoolTime <= skillWaitTime)
				canUseSkill = true;
		}
	}
}
