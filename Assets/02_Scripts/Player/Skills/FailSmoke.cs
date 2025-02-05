using System;
using UnityEngine;

public class FailSmoke : MonoBehaviour
{
	private ParticleSystem[] particleSystems;

	private void Awake()
	{
		// 하위에 있는 모든 ParticleSystem 찾기 (비활성화된 오브젝트 포함)
		particleSystems = GetComponentsInChildren<ParticleSystem>(true);
	}

	private void OnEnable()
	{
		// 오브젝트가 활성화될 때 모든 파티클 시스템 재생
		foreach (var ps in particleSystems)
		{
			ps.Play();
		}

		Invoke(nameof(Disable), 1f);

	}

	void Disable()
	{
		gameObject.SetActive(false);
	}
}
