using UnityEngine;

public class Magma : MonoBehaviour
{
	private float damageTimer;

	private void Start()
	{
		Destroy(this.gameObject, 10.0f);
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			damageTimer += Time.deltaTime;

			if (damageTimer >= 1f)
			{
				other.GetComponent<Player>()?.Damaged(1);
				damageTimer = 0f;
			}
		}
	}
}
