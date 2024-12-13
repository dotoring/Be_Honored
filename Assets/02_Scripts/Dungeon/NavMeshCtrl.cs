using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshCtrl : MonoBehaviour
{
	NavMeshSurface surface;

	private void Start()
	{
		surface = GetComponent<NavMeshSurface>();
	}

	public void BakeSurface()
	{
		surface.BuildNavMesh();
	}
}
