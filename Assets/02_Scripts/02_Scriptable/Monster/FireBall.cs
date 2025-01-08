using NUnit.Framework;
using UnityEngine;

public class FireBall : MonoBehaviour
{
	[SerializeField] private float damage;
	[SerializeField]private GameObject target;
	[SerializeField]private GameObject floor;
	[SerializeField] private Vector3 startPoint;
	[SerializeField]private float speed;

	public void InitBall(float damaged,GameObject target,Transform startPoint)
	{
		damage = damaged;
		this.target = target;
		this.startPoint=startPoint.position;
		speed = Vector3.Distance(target.transform.position, this.startPoint)/2.5f;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, target.transform.position);
	}

	private void Update()
	{
		transform.position += speed * Time.deltaTime * (target.transform.position - startPoint).normalized;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("FirePoint"))
		{
			Destroy(target);
			Instantiate(floor, target.transform.position,Quaternion.identity);
			Destroy(gameObject);
		}
		if(other.CompareTag("Player"))
		{
			Destroy(target);
			other.GetComponent<HitPlayer>()?.Damaged(damage);
			//마그마 생성 플레이어 충돌 위치의 바닥으로 변경 예정
			Instantiate(floor, target.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
