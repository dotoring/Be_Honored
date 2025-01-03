using UnityEngine;

public class BossPattern : MonoBehaviour
{
	protected BossMonster bossMonster;
    public void InitPattern(BossMonster boss)
	{
		bossMonster= boss;
	}
}
