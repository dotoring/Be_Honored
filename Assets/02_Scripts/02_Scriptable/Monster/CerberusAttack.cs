using UnityEngine;

public class CerberusAttack : MonoBehaviour
{
	[SerializeField] private BossMonster boss;

	public void AttackCerberus()
	{
		boss.BossAttack();
	}

	public void ActivePattern(int skillId)
	{
		boss.StartPattern(skillId);
	}
}
