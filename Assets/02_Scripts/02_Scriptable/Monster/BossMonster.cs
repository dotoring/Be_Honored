using UnityEngine;
using System.Collections.Generic;
using Unity.Behavior;
using NUnit.Framework;
using Photon.Pun;
using Photon.Realtime;

public class BossMonster : Monster
{
	public float skillCoolTime;
	public float skillWaitTime=0;
	public bool canUseSkill;

	public List<GameObject> playerList;
	public GameObject targetPlayer;

	[SerializeField] private Animation anim;

	[SerializeField] private List<GameObject> monsterPatternObj;

	public void StartAnimationRPC(string animName)
	{
		pv.RPC(nameof(StartAnimation), RpcTarget.AllBuffered, animName);
	}

	[PunRPC]
	public void StartAnimation(string animName)
	{
		anim.Play(animName);
	}

	public void StartPatternRPC(int skillId)
	{
		pv.RPC(nameof(StartPattern), RpcTarget.All,skillId);
	}

	[PunRPC]
	public void StartPattern(int skillId)
	{
		monsterPatternObj[skillId].GetComponent<BossPattern>().InitPattern(this);
		monsterPatternObj[skillId].SetActive(true);
	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		base.OnPlayerEnteredRoom(newPlayer);
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		playerList.Clear();
		foreach (GameObject go in temp)
		{
			playerList.Add(go);
		}
	}

	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		base.OnPlayerLeftRoom(otherPlayer);
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		playerList.Clear();
		foreach (GameObject go in temp)
		{
			playerList.Add(go);
		}
	}

	protected override void Start()
	{
		base.Start();
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in temp)
		{
			playerList.Add(go);
		}
		dieEvent += () =>
		{
			print("보스죽음");
			StartAnimationRPC("death");
		};
	}
	private void Update()
	{
		if (behaviorAgent.enabled == true)
		{
			skillWaitTime += Time.deltaTime;
			if (skillCoolTime <= skillWaitTime)
				canUseSkill = true;
		}
	}

	public void ResetSkill()
	{
		canUseSkill=false;
		skillWaitTime = 0;
	}
}
