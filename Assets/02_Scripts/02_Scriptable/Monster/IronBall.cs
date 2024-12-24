using UnityEngine;

public class IronBall : MonoBehaviour
{
	private float damage;

	public void InitBall(float damage)
	{
		this.damage = damage;
	}

	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Player>()?.Damaged(damage);
	}
}
