
using System;
using UnityEngine;

public enum EquipType
{
	HEAD,
	BODY,
	LEG,
	ARM
}
public class EquipItem : ScrapItem
{
	TypeOfItem typeOfItem = TypeOfItem.EQUIP;
	[SerializeField] EquipType typeOfEquip;

	Material materialOrigin;
	[SerializeField] Material materialRed;
	bool isOnSlot = false;
	private void Awake()
	{
		materialOrigin = GetComponent<MeshRenderer>().material;
	}

	public void ItemSetter(int value, EquipType equipTypePram)
	{
		base.ItemSetter(value);
		typeOfEquip = equipTypePram;
	}

	private new void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = true;
			if (bag == null)
			{
				bag = other.gameObject;
			}
		}
		else if (other.gameObject.GetComponent<Equipment>()?.type == typeOfEquip)
		{
			isOnSlot = true;

		}
	}

	private new void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Bag"))
		{
			isInBag = false;
		}
		else if (other.gameObject.GetComponent<Equipment>()?.type == typeOfEquip)
		{
			isOnSlot = false;

		}
	}

	protected override void CheckInBag()
	{
		if (this.isInBag)
		{
			SetInBag();
		}
		else if (isOnSlot)
		{
			PutItOn();
		}
		else
		{
			PullOut();
		}

	}

	private void PutItOn()
	{

	}
}
