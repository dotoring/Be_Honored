using TMPro;
using UnityEngine;

public class ArmoryUIToTal : MonoBehaviour
{
	[SerializeField] TMP_Text TextHp;
	[SerializeField] TMP_Text TextAttack;
	[SerializeField] TMP_Text TextDefence;
	[SerializeField] TMP_Text TextEveda;
	private void OnEnable()
	{
		App.Instance.ChangeEquip += SetUI;
		SetUI();
	}
	private void OnDisable()
	{
		if (App.Instance != null) App.Instance.ChangeEquip -= SetUI;
	}

	void SetUI()
	{

		Player.Instance.ToTalStat();
		TextHp.text = Player.Instance._stat.hpmax.ToString();
		TextAttack.text = Player.Instance._stat.attack.ToString();
		TextDefence.text = Player.Instance._stat.defence.ToString();
		TextEveda.text = Player.Instance._stat.evade.ToString();
	}

}
