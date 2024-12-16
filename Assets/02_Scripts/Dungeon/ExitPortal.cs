using UnityEngine;

public class ExitPortal : MonoBehaviour
{
	ExitMgr exitMgr;

	public void SetExitMgr(ExitMgr _exitMgr)
	{
		exitMgr = _exitMgr;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.gameObject.CompareTag("Player"))
		{
			exitMgr.ExitDungeon();
		}
	}
}
