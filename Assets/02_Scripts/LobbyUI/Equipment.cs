using TMPro;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	public EquipType type;
	[SerializeField] TMP_Text TextHp;
	[SerializeField] TMP_Text TextAttack;
	[SerializeField] TMP_Text TextDefence;
	[SerializeField] TMP_Text TextEveda;
	private void OnEnable()
	{
		App.Instance.ChangeEquip += SetUI;
	}
	private void OnDisable()
	{
		if (App.Instance != null) App.Instance.ChangeEquip -= SetUI;
	}
	void SetUI()
	{
		EQUIPSTAT equipStat = new();
		switch (type)
		{
			case EquipType.HEAD:
				equipStat = Player.Instance._armor.Head;
				break;
			case EquipType.BODY:
				equipStat = Player.Instance._armor.Body;
				break;
			case EquipType.LEG:
				equipStat = Player.Instance._armor.Leg;
				break;
			case EquipType.ARM:
				equipStat = Player.Instance._armor.Arm;
				break;
			default:
				break;
		}

		TextHp.text = equipStat.hpmax.ToString();
		TextAttack.text = equipStat.attack.ToString();
		TextDefence.text = equipStat.defence.ToString();
		TextEveda.text = equipStat.evade.ToString();
	}

}
