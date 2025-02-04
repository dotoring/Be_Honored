using System.Collections.Generic;
using UnityEngine;

public class AttackTargetOn : MonoBehaviour
{
	[SerializeField] List<GameObject> AttackOn;
	[SerializeField] List<GameObject> warriorAttack;
	[SerializeField] List<GameObject> mageAttack;
	[SerializeField] List<Vector3> position1;
	[SerializeField] List<Vector3> position2;

	private void Awake()
	{
		if (Player.Instance.myClass == KindOfclass.Warrior)
		{
			AttackOn = warriorAttack;
		}
		else if (Player.Instance.myClass == KindOfclass.Mage)
		{
			AttackOn = mageAttack;
		}
		foreach (var item in warriorAttack)
		{
			item.SetActive(false);
		}
		foreach (var item in mageAttack)
		{
			item.SetActive(false);
		}
		Player.Instance.Armed += CanvasOn;
		Player.Instance.Attackon = AttackOn;
		Player.Instance.UnArmed += CanVasOff;
	}

	private void OnDisable()
	{
		if (Player.Instance is not null)
		{
			Player.Instance.Armed -= CanvasOn;
			Player.Instance.UnArmed -= CanVasOff;
		}
	}

	private void CanVasOff()
	{
		foreach (var item in AttackOn)
		{
			item.SetActive(false);
		}
		Player.Instance.IsArm = false;
	}

	private void CanvasOn()
	{
		foreach (var item in AttackOn)
		{
			item.SetActive(true);
		}
		Player.Instance.IsArm = true;
	}
}
