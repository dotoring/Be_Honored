using UnityEngine;

public class IronBall : MonoBehaviour
{
	private float damage;
	[SerializeField] private Vector3 endPoint;

	public void InitBall(float damage,Vector3 endPoint)
	{
		this.damage = damage;
		this.endPoint = endPoint;
	}

	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Player>()?.Damaged(damage);
	}
}
