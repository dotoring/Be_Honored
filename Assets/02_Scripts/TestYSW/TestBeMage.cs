using System;
using UnityEngine;
using UnityEngine.UI;

public class TestBeMage : MonoBehaviour
{
	[SerializeField] Button button;

	private void Start()
	{
		button.onClick.AddListener(() =>
		{
			Player.Instance.myClass = KindOfclass.Mage;
		});
	}
}
