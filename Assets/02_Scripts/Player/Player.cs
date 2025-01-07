using System;
using Unity.Mathematics;
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


public partial class Player : Singleton<Player>
{
	[SerializeField] KindOfclass _myClass;
	/// <summary>
	/// current totoal stat
	/// </summary>
	public EQUIPSTAT _stat;
	/// <summary>
	/// naked body stat;
	/// </summary>
	[SerializeField] EQUIPSTAT bodyStat;

	public PlayerEquipMent _armor;
	[SerializeField] float hp = 50;
	[SerializeField] AudioClip hited;
	public Image hpBar;
	AudioSource audioSource;
	EQUIPSTAT playerstat;

	internal bool IsArm;
	public Action Armed;
	public Action UnArmed;


	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		Instance.LoadPlayerData();
	}



	public void Damaged(float damage)
	{
		if (CheckEvade())
		{
			return;
		}
		hp -= math.max(damage - _stat.defence, 1);
		//audioSource.PlayOneShot(hited);
		Debug.Log($" Player {damage} Damaged remain {hp}");
		if (hp <= 0)
		{
			App.Instance.Resetposition.Invoke();
		}

		hpBar.fillAmount = hp / 50;
	}

	private bool CheckEvade()
	{
		int roll = UnityEngine.Random.Range(0, 100);
		if (roll > _stat.evade * 2)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// summary stat in equip + body
	/// </summary>
	public void ToTalStat()
	{

		_stat = _armor.Head + _armor.Body + _armor.Leg + _armor.Arm + bodyStat;
	}

	public void Enterroom()
	{
		hp += _stat.hpmax * 10;
	}

}
