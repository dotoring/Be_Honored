using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public enum EquipType
{
	HEAD,
	BODY,
	LEG,
	ARM
}

// VendorSellPoint
public class DragEquip : MonoBehaviour
{
	[SerializeField] Material matRed;
	[SerializeField] Material matGray;
	[SerializeField] MeshRenderer mrenderer;
	[SerializeField] LayerMask targetlayer;
	[SerializeField] EquipType equipPart;

	public Transform parentToReturnTo = null;
	XRGrabInteractable xRGrabInteractable;
	bool OnSellPoint = false;
	[SerializeField] ItemManager itemManager;


	private void Awake()
	{
		mrenderer = GetComponent<MeshRenderer>();
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
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
			Destroy(gameObject);
		}
		else
		{
			Debug.Log("Return Object");
			itemManager.AddItem(gameObject);
			// transform.SetParent(parentToReturnTo);
			// transform.parent = parentToReturnTo;
			// transform.rotation = Quaternion.identity;
		}
	}

	void OnSelectEnter()
	{
		itemManager.RemoveItem(gameObject);

	}
	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log($" other type is  {other.GetComponent<Equipment>().type.ToString()} , equipPart is {equipPart.ToString()}");
		if ((((1 << other.gameObject.layer) & targetlayer) != 0) && other.GetComponent<Equipment>().type == equipPart)
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
