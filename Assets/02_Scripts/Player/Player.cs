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


public partial class Player : MonoBehaviour
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
		if (CheckEvade())
		{
			return;
		}
		hp -= math.max(damage - _stat.defence, 1);
		audioSource.PlayOneShot(hited);
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
	void ToTalStat()
	{

		_stat = _armor.Head + _armor.Body + _armor.Leg + _armor.Arm + bodyStat;
	}

	public void Enterroom()
	{
		hp += _stat.hpmax * 10;
	}

}
