using NUnit.Framework;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	[SerializeField]private BossMonster boss;
	[SerializeField]private GameObject target;
	[SerializeField]private GameObject floor;
	[SerializeField] private Transform startPoint;
	[SerializeField]private float speed;

	public void InitBall(BossMonster bossMonster,GameObject target,Transform startPoint)
	{
		boss = bossMonster;
		this.target = target;
		this.startPoint=startPoint;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, target.transform.position);
	}

	private void Update()
	{
		transform.position += speed * Time.deltaTime * (target.transform.position - startPoint.position).normalized;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("FirePoint")||other.CompareTag("Player"))
		{
			Destroy(target);
			Instantiate(floor, target.transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
