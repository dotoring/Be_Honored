using UnityEngine;

public class CerberusAttack : MonoBehaviour
{
	[SerializeField] private BossMonster boss;

	public void AttackCerberus()
	{
		boss.BossAttack();
	}
}
