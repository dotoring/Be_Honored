using System.Collections.Generic;
using UnityEngine;

public class TestPattern : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		//CreateSectorMesh(1f, 30f, 30);
		Create3DSectorMesh(3f, 90f, 10f, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void Create3DSectorMesh(float radius, float angle, float height, int segmentCount)
	{
		Mesh mesh = new Mesh();

		// 정점 리스트
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();

		// 상단과 하단의 중심 정점
		vertices.Add(new Vector3(0, height / 2, 0)); // 상단 중심 (0번 정점)
		vertices.Add(new Vector3(0, -height / 2, 0)); // 하단 중심 (1번 정점)

		// 상단 면과 하단 면 정점 생성
		float segmentAngle = angle / segmentCount;

		for (int i = 0; i <= segmentCount; i++)
		{
			float currentAngle = -angle / 2 + segmentAngle * i;
			Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward;

			// 상단 정점
			vertices.Add(direction * radius + Vector3.up * (height / 2));
			// 하단 정점
			vertices.Add(direction * radius + Vector3.down * (height / 2));
		}

		// 상단 면 삼각형 생성
		for (int i = 0; i < segmentCount; i++)
		{
			triangles.Add(0);
			triangles.Add(2 + i * 2);
			triangles.Add(2 + (i + 1) * 2);
		}

		// 하단 면 삼각형 생성
		for (int i = 0; i < segmentCount; i++)
		{
			triangles.Add(1);
			triangles.Add(3 + (i + 1) * 2);
			triangles.Add(3 + i * 2);
		}

		// 측면 삼각형 생성
		for (int i = 0; i < segmentCount; i++)
		{
			int topLeft = 2 + i * 2;
			int topRight = 2 + (i + 1) * 2;
			int bottomLeft = 3 + i * 2;
			int bottomRight = 3 + (i + 1) * 2;

			// 첫 번째 삼각형 (상단 왼쪽 - 하단 왼쪽 - 하단 오른쪽)
			triangles.Add(topLeft);
			triangles.Add(bottomLeft);
			triangles.Add(bottomRight);

			// 두 번째 삼각형 (상단 왼쪽 - 하단 오른쪽 - 상단 오른쪽)
			triangles.Add(topLeft);
			triangles.Add(bottomRight);
			triangles.Add(topRight);
		}

		// Mesh 구성
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();

		// MeshFilter와 MeshRenderer 추가
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

		meshFilter.mesh = mesh;
		meshRenderer.material = new Material(Shader.Find("Standard"));
	}

	void CreateSectorMesh(float radius, float angle, int segmentCount)
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

		// MeshRenderer와 MeshFilter 추가
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

		meshFilter.mesh = mesh;
		meshRenderer.material = new Material(Shader.Find("Standard"));
	}

}
