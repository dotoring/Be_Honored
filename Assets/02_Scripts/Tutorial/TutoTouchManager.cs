using System;
using UnityEngine;

public class TutoTouchManager : PlayerTouchManager
{
	public Action hit;

	private void Start()
	{
		triggerAreas = Player.Instance.Attackon;
		// 게임 시작 시 currentOrder에 맞는 공만 파랑으로 설정
		SetAreaToColors(currentOrder);
		SetPositionChange();
	}
	protected override void Attack()
	{
		if (!Player.Instance.IsArm) return;
		//audioSource.PlayOneShot(attacksound);  // Todo Test Code for attack

		Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y, sizeOfBox / 2, Quaternion.identity, m_LayerMask);

		foreach (var item in hitColliders)
		{
			if (item.name.Equals("SandBag"))
			{
				item.GetComponent<TutoSandBag>().Damaged();
				audioSource.PlayOneShot(attacksound);
				hit?.Invoke();
			}
		}
	}

}
