using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkillMgr : MonoBehaviour
{
	[SerializeField] List<GameObject> triggerAreas;
	public Material matgray;
	public Material matred;

	[SerializeField] GameObject ball;
	[SerializeField] GameObject box;

	List<int> pattern = new List<int>();
	Coroutine timerCoroutine;
	bool isShot = false;
	[SerializeField] InputActionProperty triggerInput;
	[SerializeField] Transform shotPoint;

	private void Start()
	{
		triggerAreas = Player.Instance.Attackon;

		// 시작 시 모든 트리거 영역의 재질을 matgray로 설정 (추가)
		ResetMaterials();
	}

	private void Update()
	{
		var triggerVal = triggerInput.action.ReadValue<float>();
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
			case "012345": //파이어볼
				GameObject go = Instantiate(ball, shotPoint.position, Quaternion.identity);
				go.GetComponent<Rigidbody>().AddForce(shotPoint.forward * 200f);
				break;
			case "1245":
				Instantiate(box, shotPoint.position, Quaternion.identity);
				break;
			case "0523": //전기충격
				break;
			default: //실패 시
				Debug.Log("실패");
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
