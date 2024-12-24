using UnityEngine;

public class TestBagPortal : MonoBehaviour
{
	[SerializeField] GameObject bagPref;

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("BagSensor"))
		{
			if(BagCtrl.Instance == null)
			{
				Instantiate(bagPref, transform.position, Quaternion.identity);
			}
			else
			{
				BagCtrl.Instance.gameObject.transform.position = transform.position;
			}
		}
	}
}
