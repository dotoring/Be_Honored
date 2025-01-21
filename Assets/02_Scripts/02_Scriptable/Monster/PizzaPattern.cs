using System.Collections.Generic;
using UnityEngine;

public class PizzaPattern : BossPattern
{
	[SerializeField]private float angle = 60.0f;
	[SerializeField]private float radius = 10.0f;
	[SerializeField] MeshFilter meshFilter;
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] private List<GameObject> playerInConelist = new();
	[SerializeField] private Material patMat;

	[SerializeField] private float curTime;


	private void OnEnable()
	{
		CreateSectorMesh(radius, angle, 30,6f);
		transform.localPosition = new Vector3(0, -3f, 1.4f);
		curTime = 0;
	}

	private void Update()
	{
		curTime += Time.deltaTime;
		if (curTime < 2.0f)
		{
			transform.localPosition += 2 * Time.deltaTime * Vector3.up;
		}
		DetectTargetsInCone();
		if(curTime>=3.0f)
		{
			foreach (GameObject obj in playerInConelist)
			{
				obj.GetComponentInChildren<HitPlayer>()?.Damaged(10);
			}
			//플레이어 리스트에 남은 플레이어들의 hp를 깎는 로직
			gameObject.SetActive(false);
		}
	}
	public void CreateSectorMesh(float radius, float angle, int segmentCount,float height)//부채꼴 3D 오브젝트 생성
	{
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();

		// 정점 및 삼각형 개수 계산
		int vertexCount = (segmentCount + 1) * 2 + 2; // 외곽 정점 + 윗면/아랫면 중심
		int triangleCount = segmentCount * 4 * 3 + 2 * 6; // 윗면, 아랫면, 곡선 옆면, 중심 옆면

		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[triangleCount];
		Vector2[] uv = new Vector2[vertexCount];

		float angleStep = angle / segmentCount * Mathf.Deg2Rad;

		// 외곽 정점 생성 (윗면과 아랫면)
		for (int i = 0; i <= segmentCount; i++)
		{
			float currentAngle = -angle / 2 * Mathf.Deg2Rad + i * angleStep;
			float z = Mathf.Cos(currentAngle) * radius;
			float x = Mathf.Sin(currentAngle) * radius;

			// 아랫면 정점
			vertices[i] = new Vector3(x, -3, z);
			uv[i] = new Vector2((x / radius + 1) / 2, (z / radius + 1) / 2);

			// 윗면 정점
			vertices[i + segmentCount + 1] = new Vector3(x, height-3, z);
			uv[i + segmentCount + 1] = new Vector2((x / radius + 1) / 2, (z / radius + 1) / 2);
		}

		// 중심 정점 추가 (윗면과 아랫면)
		vertices[vertexCount - 2] = new Vector3(0, height-3, 0); // 윗면 중심
		uv[vertexCount - 2] = new Vector2(0.5f, 0.5f);

		vertices[vertexCount - 1] = new Vector3(0, -3, 0); // 아랫면 중심
		uv[vertexCount - 1] = new Vector2(0.5f, 0.5f);

		// 윗면 삼각형 생성
		int triangleIndex = 0;
		for (int i = 0; i < segmentCount; i++)
		{
			triangles[triangleIndex++] = vertexCount - 2; // 윗면 중심
			triangles[triangleIndex++] = i + segmentCount + 1; // 현재 정점
			triangles[triangleIndex++] = i + segmentCount + 2; // 다음 정점
		}

		// 아랫면 삼각형 생성
		for (int i = 0; i < segmentCount; i++)
		{
			triangles[triangleIndex++] = vertexCount - 1; // 아랫면 중심
			triangles[triangleIndex++] = i + 1; // 다음 정점
			triangles[triangleIndex++] = i; // 현재 정점
		}

		// 곡선 옆면 삼각형 생성
		for (int i = 0; i < segmentCount; i++)
		{
			// 첫 번째 삼각형
			triangles[triangleIndex++] = i;
			triangles[triangleIndex++] = i + 1;
			triangles[triangleIndex++] = i + segmentCount + 1;

			// 두 번째 삼각형
			triangles[triangleIndex++] = i + 1;
			triangles[triangleIndex++] = i + segmentCount + 2;
			triangles[triangleIndex++] = i + segmentCount + 1;
		}

		// 중심으로 연결되는 삼각형 옆면 생성
		// 왼쪽 끝면
		triangles[triangleIndex++] = vertexCount - 1; // 아랫면 중심
		triangles[triangleIndex++] = 0; // 아랫면 첫 번째 정점
		triangles[triangleIndex++] = vertexCount - 2; // 윗면 중심

		triangles[triangleIndex++] = vertexCount - 2; // 윗면 중심
		triangles[triangleIndex++] = 0; // 아랫면 첫 번째 정점
		triangles[triangleIndex++] = segmentCount + 1; // 윗면 첫 번째 정점

		// 오른쪽 끝면
		triangles[triangleIndex++] = vertexCount - 1; // 아랫면 중심
		triangles[triangleIndex++] = segmentCount; // 아랫면 마지막 정점
		triangles[triangleIndex++] = vertexCount - 2; // 윗면 중심

		triangles[triangleIndex++] = vertexCount - 2; // 윗면 중심
		triangles[triangleIndex++] = segmentCount; // 아랫면 마지막 정점
		triangles[triangleIndex++] = segmentCount + segmentCount + 1; // 윗면 마지막 정점

		// Mesh 설정
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;

		// 법선 벡터 계산
		mesh.RecalculateNormals();

		// Mesh 적용
		meshFilter.mesh = mesh;
		meshRenderer.material = patMat;

	}

	void DetectTargetsInCone()
	{
		foreach (GameObject target in bossMonster.

			playerList)
		{
			Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

			// 부채꼴 중심 방향과 타겟 방향 간의 각도를 계산
			float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
			float dis = Vector3.Distance(transform.position, target.transform.position);
			// 각도가 범위 안에 있으면 감지된 타겟으로 판단
			if (angleToTarget <= angle / 2f && dis <= radius)
			{
				// 타겟이 범위 안에 있음
				if (!playerInConelist.Contains(target.transform.root.gameObject))
					playerInConelist.Add(target.transform.root.gameObject);
				//같은 이름의 객체가 있으면 넣지 않음
			}
			else
			{
				if (playerInConelist.Contains(target.transform.root.gameObject))
					playerInConelist.Remove(target.transform.root.gameObject);
			}
		}

		// 감지된 타겟들 출력 (디버깅용)
		foreach (GameObject target in playerInConelist)
		{
			Debug.Log($"Detected Target: {target.name}");
		}
	}
}
