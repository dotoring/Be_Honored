using UnityEngine;

public class PlayerSpawnMgr : MonoBehaviour
{
	[SerializeField] GameObject xrOrigin;
	Transform spawnPoint;
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
	}
}
