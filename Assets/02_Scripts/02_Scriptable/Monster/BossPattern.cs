using UnityEngine;

public class BossPattern : MonoBehaviour
{
	[SerializeField]protected BossMonster bossMonster;
    public void InitPattern(BossMonster boss)
	{
		if(bossMonster==null)
			bossMonster= boss;
	}
}
