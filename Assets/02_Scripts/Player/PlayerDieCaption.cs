using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDieCaption : MonoBehaviour
{
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] TMP_Text text;
	Color backCol;
	Color textCol;

	private void Start()
	{
		backCol = spriteRenderer.color;
		textCol = text.color;
		backCol.a = 0;
		textCol.a = 0;
		spriteRenderer.color = backCol;
		text.color = textCol;
		Player.Instance.OnPlayerDie += CaptionOn;
	}

	private void CaptionOn()
	{
		backCol.a = 1;
		textCol.a = 1;


		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		backCol.a = 1;
		textCol.a = 1;
		spriteRenderer.color = backCol;
		text.color = textCol;
		yield return new WaitForSeconds(1.5f);

		while (backCol.a > 0.05)
		{
			spriteRenderer.color = backCol;
			text.color = textCol;
			backCol.a -= 0.01f;
			textCol.a -= 0.01f;
			yield return null;
		}
		backCol.a = 0;
		textCol.a = 0;
		spriteRenderer.color = backCol;
		text.color = textCol;
	}
}
