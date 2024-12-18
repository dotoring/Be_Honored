using UnityEngine;
using UnityEngine.Rendering;

public class LazerPattern : MonoBehaviour
{
	[SerializeField] private BossMonster bossMonster;
	[SerializeField] private float rotateAngle;
	[SerializeField] private GameObject range;
	[SerializeField] private GameObject syl;

	[SerializeField] private float chargingTime;
	[SerializeField] private float lazingTime;


	private void Update()
	{
		if (chargingTime < 2.0f)
		{
			chargingTime += Time.deltaTime;
			transform.position += Vector3.up*2*Time.deltaTime;
		}
		else
		{
			range.SetActive(false);
			syl.SetActive(true);
			if (lazingTime < 2.0f)
			{
				lazingTime += Time.deltaTime;
				transform.root.transform.eulerAngles += rotateAngle * Time.deltaTime * Vector3.up;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}


	public void InitThis(BossMonster bossmonster, float angle)
	{
		bossMonster = bossmonster;
		rotateAngle = angle;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (chargingTime < 2.0f)
			{
				print("경고");
			}
			else
			{
				print("데미지");
			}
		}
	}
}
