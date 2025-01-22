using UnityEngine;

public class UsableItem : ScrapItem
{
	protected virtual void TakePotion()
	{
		Player.Instance.Heal(20);
		Destroy(gameObject);
	}

	protected override void OnTriggerEnter(Collider other)
	{
		base.OnTriggerEnter(other);
		if (other.CompareTag("Mouse"))
		{
			TakePotion();
		}
	}
}
