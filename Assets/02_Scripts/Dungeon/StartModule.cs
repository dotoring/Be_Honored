using UnityEngine;

public class StartModule : MonoBehaviour
{
	[SerializeField] Transform playerSpawnPoint;

    void Start()
    {
        
    }

	public Transform GetSpawnPoint()
	{
		return playerSpawnPoint;
	}
}
