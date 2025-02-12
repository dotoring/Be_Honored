using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
	[SerializeField] private Button startBtn;
	[SerializeField] private Button tutoBtn;
	[SerializeField] private Button quitBtn;

	private void Start()
	{
		startBtn.onClick.AddListener(() =>
		{
			SceneManager.LoadScene("lobbySample_Working1");
		});

		tutoBtn.onClick.AddListener(() =>
		{
			SceneManager.LoadScene("Tutorial");
		});

		quitBtn.onClick.AddListener(() =>
		{
			Application.Quit();
		});
	}
}
