using UnityEngine;
using UnityEngine.UIElements;

public class Stone : MonoBehaviour
{
	[SerializeField] private StonePattern stp;
	[SerializeField] private Transform point;
 	[SerializeField] private float waitTime;
	[SerializeField] private float curTime;
	[SerializeField] private float damage;
	[SerializeField] private float speed;
	[SerializeField] private float dis;
	[SerializeField] private bool chek;

	public void InitStone(StonePattern stp,Transform point,float waitTime, float damage,float speed)
	{
		this.stp = stp;
		this.point = point;
		this.waitTime=waitTime;
		this.damage=damage;
		this.speed=speed;
	}

	private void OnEnable()
	{
		curTime = 0;
	}
	private void OnDisable()
	{
		print(gameObject.name+"disable");
	}
	private void Update()
	{
		if (curTime < waitTime)
			curTime += Time.deltaTime;
		else
		{
			transform.position += -speed * Time.deltaTime * transform.up;
			dis = Vector3.Distance(transform.position, point.position);
			if (dis <= 0.1f&&chek==false)
			{
				chek = true;
				stp.endPattern.Invoke();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<Player>()?.Damaged(damage);
	}
}
