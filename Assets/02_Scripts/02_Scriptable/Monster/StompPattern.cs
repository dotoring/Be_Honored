using NUnit.Framework.Internal;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StompPattern : MonoBehaviour
{
	[SerializeField] private BossMonster bossMonster;
	[SerializeField] MeshFilter meshFilter;
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] private Material patMat;
	[SerializeField] private List<GameObject> targets;
	[SerializeField] private float curTime = 0.0f;
	[SerializeField] private float chargingTime=3.0f;

	private void OnEnable()
	{
		curTime = 0;
		targets.Clear();
		CreateSectorMesh(3.0f, 360.0f, 300);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player))
		{
			targets.Add(player.gameObject);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<Player>(out Player player))
		{
			targets.Remove(player.gameObject);
		}
	}

	private void Update()
	{
		curTime+= Time.deltaTime;
		if(curTime>=chargingTime)
		{
			gameObject.SetActive(false);
		}
	}


	private void OnDisable()
	{
		foreach (var target in targets)
		{
			target.GetComponent<Player>()?.Damaged(bossMonster.attackPower*2);
		}
	}


	public void CreateSectorMesh(float radius, float angle, int segmentCount)
	{
		Mesh mesh = new Mesh();

		// 정점 설정
		List<Vector3> vertices = new List<Vector3>();
		vertices.Add(Vector3.zero); // 중심점
		float segmentAngle = angle / segmentCount;

		for (int i = 0; i <= segmentCount; i++)
		{
			float currentAngle = -angle / 2 + segmentAngle * i;
			Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;
			vertices.Add(direction * radius);
		}

		// 삼각형 설정
		List<int> triangles = new List<int>();
		for (int i = 1; i < vertices.Count - 1; i++)
		{
			triangles.Add(0);
			triangles.Add(i);
			triangles.Add(i + 1);
		}

		// Mesh 적용
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;
		meshRenderer.material = patMat;
	}
}
