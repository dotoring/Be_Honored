using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
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
	public KindOfclass myClass;
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
	public Material hpBar_V2;
	public AudioSource audioSource;
	EQUIPSTAT playerstat;

	internal bool IsArm;
	public Action Armed;
	public Action UnArmed;
	[SerializeField] private AudioClip[] bgms;

	public Action OnPlayerDie;

	protected override void Awake()
	{
		base.Awake();
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		Instance.LoadPlayerData();

		audioSource.loop = true;
		ToTalStat();
		hp = _stat.hpmax;
		// ChangeBGM(0);
		hpBar_V2.SetFloat("_Fill_Height", -0.5f);
	}



	public void Damaged(float damage)
	{
		if (CheckEvade())
		{
			return;
		}
		hp -= math.max(damage - _stat.defence, 1);
		audioSource.PlayOneShot(hited);
		Debug.Log($" Player {damage} Damaged remain {hp}");
		if (hp <= 0)
		{
			App.Instance.Resetposition.Invoke();  // TODO : DEAD Action;
			OnPlayerDie.Invoke();
			hp = _stat.hpmax;
		}

		//hpBar.fillAmount = hp / _stat.hpmax;
		hpBar_V2.SetFloat("_Fill_Height", 0.5f - (hp / _stat.hpmax));

	}

	public void Heal(float heal)
	{
		hp += heal;
		if (hp > _stat.hpmax)
		{
			hp = _stat.hpmax;
		}

		//hpBar.fillAmount = hp / _stat.hpmax;
		hpBar_V2.SetFloat("_Fill_Height", 0.5f - (hp / _stat.hpmax));
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
		hp = _stat.hpmax * 10 + bodyStat.hpmax;
	}

	public void ChangeBGM(int sceneNumber)
	{
		if (sceneNumber == 0)
		{
			audioSource.Stop();
			audioSource.clip = bgms[0];
			audioSource.Play();
		}
		else if (sceneNumber == 1)
		{
			audioSource.Stop();
			audioSource.clip = bgms[1];
			audioSource.Play();
		}
		else if(sceneNumber==2)
		{
			audioSource.Stop();
			audioSource.clip = bgms[2];
			audioSource.Play();
		}
	}

	protected override void OnApplicationQuit()
	{
		Instance.SavePlayerData();
	}


}
