using Photon.Pun;
using UnityEngine;

public class ExitPortal : MonoBehaviour
{
	[SerializeField] ExitMgr exitMgr;
	[SerializeField] bool isToLobby;
	public void SetExitMgr(ExitMgr _exitMgr)
	{
		exitMgr = _exitMgr;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log(other.tag);
		if (other.CompareTag("Player"))
		{
			if(other.GetComponent<PhotonView>().IsMine)
			{
				if (isToLobby)
				{
					exitMgr.ExitDungeon();
				}
				else
				{
					exitMgr.TeleportToUnderStage();
				}
			}
		}
	}
}
