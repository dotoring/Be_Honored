using UnityEngine;

public class TestBagPortal : MonoBehaviour
{

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "BagSensor")
		{
			BagCtrl.Instance.gameObject.transform.position = transform.position;
		}
	}
}
