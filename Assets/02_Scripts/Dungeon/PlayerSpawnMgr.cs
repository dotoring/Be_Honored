using UnityEngine;
using Photon.Pun;

public class PlayerSpawnMgr : MonoBehaviour
{
	[SerializeField] GameObject xrOrigin;
	Transform spawnPoint;

	[SerializeField] PlayerPositionTest pp;

	private void Awake()
	{
		App.Instance.Resetposition += Respawn;
	}

	public void SetSpawnPoint(Transform point)
	{
		spawnPoint = point;
		SpawnPlayer();
	}

	void SpawnPlayer()
	{
		xrOrigin.transform.position = spawnPoint.position;

		GameObject go = PhotonNetwork.Instantiate("Player", spawnPoint.position, Quaternion.identity);
		go.GetComponent<PlayerTracker>().pp = this.pp;
	}

	void Respawn()
	{
		xrOrigin.transform.position = spawnPoint.position;
	}

	private void OnDestroy()
	{
		App.Instance.Resetposition -= Respawn;
	}
}
