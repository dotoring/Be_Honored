using System;
using CrusaderUI.Scripts;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public enum KindOfclass
{
	Warrior,
	Mage,
	Archer,
	Priest
}

public struct Stat
{
	int HP;
	int Attack;
	int defence;
	int evade;

}


public partial class Player : MonoBehaviour
{
	[SerializeField] KindOfclass _myClass;
	/// <summary>
	/// current totoal stat
	/// </summary>
	[SerializeField] EQUIPSTAT _stat;
	/// <summary>
	/// naked body stat;
	/// </summary>
	[SerializeField] EQUIPSTAT bodyStat;
	public PlayerEquipMent _armor;
	float hp = 50;
	[SerializeField] AudioClip hited;
	[SerializeField] Image hpBar;
	AudioSource audioSource;
	EQUIPSTAT playerstat;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		App.Instance.player = this;
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

		hpBar.fillAmount = hp / 50;
	}

	/// <summary>
	/// summary stat in equip + body
	/// </summary>
	void ToTalStat()
	{

		_stat = _armor.Head + _armor.Body + _armor.Leg + _armor.Arm + bodyStat;
	}

}
