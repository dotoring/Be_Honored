using UnityEngine;

public class StartModule : MonoBehaviour
{
	[SerializeField] Transform playerSpawnPoint;

	public Transform GetSpawnPoint()
	{
		return playerSpawnPoint;
	}
}
