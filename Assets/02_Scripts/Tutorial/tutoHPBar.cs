using System;
using UnityEngine;

public class tutoHPBar : MonoBehaviour
{
	internal Action touch;

	private void OnTriggerEnter(Collider other)
	{
		touch?.Invoke();
		Debug.Log($" tutohpbar trigger");
	}
}
