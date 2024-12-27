using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class PlayerSkillMgr : MonoBehaviour
{
	public List<GameObject> triggerAreas;
	public Material matgray;
	public Material matred;

	[SerializeField] GameObject ball;
	[SerializeField] GameObject box;

	List<int> pattern = new List<int>();
	Coroutine timerCoroutine;
	bool isShot;
	[SerializeField]
	XRInputValueReader<float> m_TriggerInput = new XRInputValueReader<float>("Trigger");

	private void Start()
	{
		// 시작 시 모든 트리거 영역의 재질을 matgray로 설정 (추가)
		ResetMaterials();
	}

	private void Update()
	{
        var triggerVal = m_TriggerInput.ReadValue();
		if(triggerVal > 0.1f && !isShot)
		{
			UseSkill();
			ResetMaterials();
			isShot = true;
		}
		else if(triggerVal < 0.1f && isShot)
		{
			isShot = false;
		}
	}

	private void UseSkill()
	{
		string skill = string.Join("", pattern);
		pattern.Clear();

		switch (skill)
		{
			case "012345":
				Instantiate(ball, transform.position, Quaternion.identity);
				break;
			case "0235":
				Instantiate(box, transform.position, Quaternion.identity);
				break;
		}
	}

	// 모든 트리거 영역의 재질을 matgray로 되돌리는 함수 (추가)
	private void ResetMaterials()
	{
		foreach (GameObject area in triggerAreas)
		{
			area.GetComponent<Renderer>().material = matgray;
		}
	}

	void StartTimer()
	{
		if(timerCoroutine != null)
		{
			StopCoroutine(timerCoroutine);
		}

		timerCoroutine = StartCoroutine(Timer(2f));
	}

	IEnumerator Timer(float duration)
	{
		yield return new WaitForSeconds(duration);

		ResetMaterials();
		pattern.Clear();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (triggerAreas.Contains(other.gameObject))
		{
			int enteredIndex = triggerAreas.IndexOf(other.gameObject);
			other.GetComponent<Renderer>().material = matred;

			pattern.Add(enteredIndex);
			StartTimer();
		}
	}
}
