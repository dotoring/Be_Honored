using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TestItem : MonoBehaviour
{
	XRGrabInteractable interactable;
	Rigidbody rb;
	Collider col;
	GameObject bag;
	bool isInBag;

	private void Start()
	{
		interactable = GetComponent<XRGrabInteractable>();
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();

		interactable.selectEntered.AddListener((arg) =>
		{
			col.isTrigger = true;
		});

		interactable.selectExited.AddListener((arg) =>
		{
			col.isTrigger = false;
			CheckInBag();
		});
	}

	void CheckInBag()
	{
		if (isInBag)
		{
			SetInBag();
		}
		else
		{
			PullOut();
		}
	}

	void SetInBag()
	{
		rb.isKinematic = true;
		rb.useGravity = false;

		transform.SetParent(bag.transform, true);
	}

	void PullOut()
	{
		rb.isKinematic = false;
		rb.useGravity = true;

		transform.SetParent(null, true);
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("test");
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = true;
			if (bag == null)
			{
				bag = other.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = false;
		}
	}
}
