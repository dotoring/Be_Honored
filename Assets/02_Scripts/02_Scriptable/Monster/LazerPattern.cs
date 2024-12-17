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
		transform.root.transform.eulerAngles+=rotateAngle * Time.deltaTime*Vector3.up;
	}


	public void InitThis(BossMonster bossmonster, float angle)
	{
		bossMonster = bossmonster;
		rotateAngle = angle;
	}
}
