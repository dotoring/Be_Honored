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
	}
	private void OnDisable()
	{
		if (App.Instance != null) App.Instance.ChangeEquip -= SetUI;
	}

	void SetUI()
	{

		App.Instance.player.ToTalStat();
		TextHp.text = App.Instance.player._stat.hpmax.ToString();
		TextAttack.text = App.Instance.player._stat.attack.ToString();
		TextDefence.text = App.Instance.player._stat.defence.ToString();
		TextEveda.text = App.Instance.player._stat.evade.ToString();
	}

}
