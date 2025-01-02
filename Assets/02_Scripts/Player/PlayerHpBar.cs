using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
	private void OnEnable()
	{
		Player.Instance.hpBar = this.GetComponent<Image>();
	}

}
