using System.Collections.Generic;
using UnityEngine;

public class AttackTargetOn : MonoBehaviour
{
	[SerializeField] List<GameObject> AttackOn;
	[SerializeField] List<Vector3> position1;
	[SerializeField] List<Vector3> position2;

	private void Awake()
	{
		foreach (var item in AttackOn)
		{
			item.SetActive(false);
		}
		Player.Instance.Armed += CanvasOn;
		Player.Instance.Attackon = AttackOn;
		Player.Instance.UnArmed += CanVasOff;
	}

	private void OnDisable()
	{
		Player.Instance.Armed -= CanvasOn;
		Player.Instance.UnArmed -= CanVasOff;
	}

	private void CanVasOff()
	{
		foreach (var item in AttackOn)
		{
			item.SetActive(false);
		}
		Player.Instance.IsArm = false;
	}

	private void CanvasOn()
	{
		foreach (var item in AttackOn)
		{
			item.SetActive(true);
		}
		Player.Instance.IsArm = true;
	}
}
