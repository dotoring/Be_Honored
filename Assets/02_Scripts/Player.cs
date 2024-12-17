using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum KindOfclass
{
	Warrior,
	Mage,
	Archer,
	Priest
}

public struct Stat
{
	int str;
	int dex;
	int inteli;

	public int Str { get => str; set => str = value; }
	public int Dex { get => dex; set => dex = value; }
	public int Inteli { get => inteli; set => inteli = value; }
}

public struct Armor
{
	public EquipmentSO head;
	public EquipmentSO Body;
	public EquipmentSO Leg;
	public EquipmentSO Arm;

}

public class Player : MonoBehaviour
{
	[SerializeField] KindOfclass _myClass;
	[SerializeField] Stat _stat;
	[SerializeField] Stat _curStat;
	[SerializeField] Armor _armor;
	float hp = 50;
	[SerializeField] AudioClip hited;
	AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}


	void ArmorChange()
	{
		SetStat(_armor.head);
		SetStat(_armor.Body);
		SetStat(_armor.Leg);
		SetStat(_armor.Arm);
	}

	private void SetStat(EquipmentSO equip)
	{
		_curStat.Str = _stat.Str + equip.str;
		_curStat.Dex = _stat.Dex + equip.dex;
		_curStat.Inteli = _stat.Inteli + equip.intelli;
	}

	public void Damaged(float damage)
	{
		hp -= damage;
		audioSource.PlayOneShot(hited);
		Debug.Log($" Player {damage} Damaged remain {hp}");
		if (hp <= 0)
		{
			App.Instance.Resetposition.Invoke();
		}
	}

}
