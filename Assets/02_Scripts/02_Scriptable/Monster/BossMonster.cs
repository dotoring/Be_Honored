using UnityEngine;
using System.Collections.Generic;
using Unity.Behavior;
using NUnit.Framework;

public class BossMonster : Monster
{
	public bool isDoorOpen;
	public float skillCoolTime;
	public float skillWaitTime=0;
	public bool canUseSkill;

	public List<GameObject> playerList;

	public GameObject targetPlayer;

	

	protected override void Start()
	{
		base.Start();
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

	public override void ActiveSelf()
	{
		base.ActiveSelf();
		isDoorOpen = true;
	}

	public void ResetSkill()
	{
		canUseSkill=false;
		skillWaitTime = 0;
	}
}
