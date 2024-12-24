using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections;

public class ExitMgr : MonoBehaviourPunCallbacks
{
	[SerializeField] Image loadingbar;
	[SerializeField] GameObject canvas;

	public void ExitDungeon()
	{
		PhotonNetwork.LeaveRoom();
	}

	public override void OnLeftRoom()
	{
		StartCoroutine(NewMethod());
	}

	IEnumerator NewMethod()
	{
		canvas.SetActive(true);
		yield return null;
		AsyncOperation aload = SceneManager.LoadSceneAsync("lobbySample_Working1");
		while (!aload.isDone)
		{
			loadingbar.fillAmount = aload.progress;
			yield return null;
		}

	}

	public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
	{
		Debug.Log("test");
	}
}
