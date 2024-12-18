using UnityEngine;
using System.Collections.Generic;
using Unity.Behavior;
using NUnit.Framework;

public class BossMonster : MonoBehaviour
{
	public MonsterSpawner spawner;
	public float detectRange;
	public float attackRange;
	public float attackPower;
	public float hp;
	public bool isDoorOpen;
	public float skillCoolTime;
	public float skillWaitTime=0;
	public bool canUseSkill;

	public List<GameObject> playerList;
	public List<Transform> fireStartPoint;

	public System.Action dieEvent;

	private void Start()
	{
		dieEvent += () => spawner.RemoveFromList(this.gameObject);
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in temp)
		{
			playerList.Add(go);
		}
	}
	private void Update()
	{

		if (isDoorOpen == true)
		{
			skillWaitTime += Time.deltaTime;
			if (skillCoolTime <= skillWaitTime)
				canUseSkill = true;
		}
	}

	public void ResetSkill()
	{
		canUseSkill=false;
		skillWaitTime = 0;
	}

	public GameObject CreateSectorMesh(float radius, float angle, int segmentCount)
	{
		GameObject newobj = new GameObject();
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
		MeshFilter meshFilter = newobj.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = newobj.AddComponent<MeshRenderer>();

		meshFilter.mesh = mesh;
		meshRenderer.material = new Material(Shader.Find("Standard"));

		return newobj;
	}

}
