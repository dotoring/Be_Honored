using UnityEngine;

public class PlayerSpawnMgr : MonoBehaviour
{
	[SerializeField] GameObject xrOrigin;
	Transform spawnPoint;

	public void SetSpawnPoint(Transform point)
	{
		spawnPoint = point;
		SpawnPlayer();
	}

	void SpawnPlayer()
	{
		xrOrigin.transform.position = spawnPoint.position;
	}
}
