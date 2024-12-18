using UnityEngine;
using Photon.Pun;

public class PlayerSpawnMgr : MonoBehaviour
{
	[SerializeField] GameObject xrOrigin;
	Transform spawnPoint;

	[SerializeField] PlayerPositionTest pp;
	[SerializeField] GameObject playerPref;

	private void Awake()
	{
		App.Instance.Resetposition += SpawnPlayer;
	}

	public void SetSpawnPoint(Transform point)
	{
		spawnPoint = point;
		SpawnPlayer();
	}

	void SpawnPlayer()
	{
		Debug.Log($" spawnPlayered");
		xrOrigin.transform.position = spawnPoint.position;

		GameObject go = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
		go.GetComponent<PlayerTracker>().pp = this.pp;
	}
}
