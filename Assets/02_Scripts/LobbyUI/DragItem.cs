using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// VendorSellPoint
public class DragItem : MonoBehaviour
{
	[SerializeField] Material matRed;
	[SerializeField] Material matGray;
	[SerializeField] MeshRenderer mrenderer;
	[SerializeField] LayerMask targetlayer;

	public Transform parentToReturnTo = null;
	XRGrabInteractable xRGrabInteractable;
	bool OnSellPoint = false;
	EquipType equipPart;
	RectTransform rectTransform;
	Rigidbody rig;
	[SerializeField] ItemManager itemManager;

	private void Awake()
	{
		rig = GetComponent<Rigidbody>();
		mrenderer = GetComponent<MeshRenderer>();
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		rectTransform = GetComponent<RectTransform>();
		//parentToReturnTo = transform.parent;
		xRGrabInteractable.selectEntered.AddListener(_ => OnSelectEnter());
		xRGrabInteractable.selectExited.AddListener(_ => OnSelectExit());
	}

	private void OnSelectExit()
	{
		if (OnSellPoint)
		{
			Debug.Log($" selling item");
			// TODO : Add Gold
			Destroy(this.gameObject);
		}
		else
		{
			Debug.Log($"Return Object");
			itemManager.AddItem(this.gameObject);
			//transform.parent = parentToReturnTo;
			// transform.rotation = Quaternion.identity;
			// rig.isKinematic = true;
			// rectTransform.rotation = Quaternion.identity;
			// rectTransform.anchoredPosition = Vector3.zero;
			// transform.SetParent(parentToReturnTo);
			// rig.isKinematic = false;
		}
	}

	void OnSelectEnter()
	{
		//parentToReturnTo = transform.parent;
		itemManager.RemoveItem(this.gameObject);

	}
	private void OnTriggerEnter(Collider other)
	{

		if (((1 << other.gameObject.layer) & targetlayer) != 0)
		{
			Debug.Log($" trigger enter ");
			mrenderer.material = matRed;
			OnSellPoint = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (((1 << other.gameObject.layer) & targetlayer) != 0)
		{

			Debug.Log($" trigger exit ");
			mrenderer.material = matGray;
			OnSellPoint = false;
		}
	}

	
}
