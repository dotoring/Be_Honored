using UnityEngine;

public class HitPlayer : MonoBehaviour
{
	public void Damaged(float damage)
	{
		Player.Instance.Damaged(damage);
	}

}
