using Photon.Pun;
using UnityEngine;

public class HowlingPattern : BossPattern
{
	[SerializeField] private float curTime;
	[SerializeField] private float attackRange;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.root.position, attackRange);
	}

	private void OnEnable()
	{
		curTime = 0;
	}

	private void Update()
	{
		curTime += Time.deltaTime;
		if(curTime>2.0f)
		{
			AttackPattern();
			gameObject.SetActive(false);
		}
	}

	private void AttackPattern()
    {
		if (PhotonNetwork.IsMasterClient)
		{
			Collider[] cols = Physics.OverlapSphere(transform.root.transform.position, attackRange, 1 << 10);
			for (int i = 0; i < cols.Length; i++)
			{
				cols[i].GetComponent<HitPlayer>()?.Damaged(bossMonster.attackPower);
			}
		}
	}
}
