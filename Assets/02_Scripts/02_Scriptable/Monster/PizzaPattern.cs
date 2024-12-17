using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PizzaPattern : MonoBehaviour
{
	[SerializeField] private BossMonster bossMonster;

	private float angle=60.0f;
	private float radius=10.0f;

	private float castingTime = 0;
	private float castTime = 3.0f;

	[SerializeField]private List<GameObject> playerInConelist = new List<GameObject>();


	public void InitThis(BossMonster bossmonster)
	{
		bossMonster = bossmonster;
	}

	private void Start()
	{
		CreateSectorMesh(radius,angle,30);
	}

	private void Update()
	{
		DetectTargetsInCone();
		if(bossMonster.canUseSkill==false)
		{
			foreach(GameObject obj in playerInConelist)
			{
				obj.GetComponentInChildren<TestPlayer>().hp -= 10;
			}
			//플레이어 리스트에 남은 플레이어들의 hp를 깎는 로직
			Destroy(gameObject);
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

		// MeshRenderer와 MeshFilter 추가
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

		meshFilter.mesh = mesh;
		meshRenderer.material = new Material(Shader.Find("Standard"));

	}

	void DetectTargetsInCone()
	{
		foreach (GameObject target in bossMonster.playerList)
		{
			Vector3 directionToTarget = (target.transform.position - transform.position).normalized;

			// 부채꼴 중심 방향과 타겟 방향 간의 각도를 계산
			float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);

			// 각도가 범위 안에 있으면 감지된 타겟으로 판단
			if (angleToTarget <= angle / 2f)
			{
				// 타겟이 범위 안에 있음
				if(!playerInConelist.Contains(target.transform.root.gameObject))
					playerInConelist.Add(target.transform.root.gameObject);
				//같은 이름의 객체가 있으면 넣지 않음
			}
			else
			{
				if(playerInConelist.Contains(target.transform.root.gameObject))
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
