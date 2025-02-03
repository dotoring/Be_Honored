using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class BossMonster : Monster
{
	public float skillCoolTime;
	public float skillWaitTime=0;
	public bool canUseSkill;

	public List<GameObject> playerList;
	public GameObject targetPlayer;

	BossState bossstate;
	[SerializeField] private Animation anim;
	public Transform resetPos;
	[SerializeField] private List<GameObject> monsterPatternObj;

	public override void OnEnable()
	{
		base.OnEnable();
		//디버그할때 주석
		playerList = DungeonMgr.instance.playerListInBoss;
		behaviorAgent.BlackboardReference.GetVariableValue("BossState", out bossstate);
	}

	public void StartAnimationRPC(string animName)
	{
		if (!PhotonNetwork.IsConnected)
			StartAnimation(animName);
		else
			pv.RPC(nameof(StartAnimation), RpcTarget.All, animName);
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
		if (monsterPatternObj[skillId].activeSelf == false)
		{
			monsterPatternObj[skillId].SetActive(true);
			monsterPatternObj[skillId].GetComponent<BossPattern>().InitPattern(this);
		}
	}

	//보스 방에 들어온 사람들을 playerlist에 추가
	public void GetPlayerList()
	{
		playerList=DungeonMgr.instance.playerListInBoss;
	}

	//플레이어가 룸을 떠날 때
	//playerlist과 서버 룸에 있는 사람과 비교해서 보스방에 있던 사람이 서버룸에 있는 사람목록에 없으면삭제
	public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
	{
		base.OnPlayerLeftRoom(otherPlayer);
		GameObject removeGo=new GameObject();
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		foreach(var go in playerList)
		{
			int check = 0;
			for(int i=0;i<temp.Length;i++)
			{
				if (go == temp[i])
				{
					check++;
					break;
				}
			}
			if(check == 0)
			{
				removeGo = go;
			}
		}
		if(removeGo!=null)
			playerList.Remove(removeGo);
	}

	protected override void Start()
	{
		base.Start();
		dieEvent += () =>
		{
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

	public void BossAttack()
	{
		print("보스공격");
		targetPlayer.GetComponent<HitPlayer>()?.Damaged(attackPower);
		transform.forward = (targetPlayer.transform.position - transform.position).normalized;
	}
}
