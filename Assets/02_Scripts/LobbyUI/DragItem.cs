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


	private void Awake()
	{
		mrenderer = GetComponent<MeshRenderer>();
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		parentToReturnTo = transform.parent;
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
			transform.SetParent(parentToReturnTo);
			transform.rotation = Quaternion.identity;
		}
	}

	void OnSelectEnter()
	{
		parentToReturnTo = transform.parent;

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
