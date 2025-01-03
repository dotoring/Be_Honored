using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class ModuleMgr : MonoBehaviour
{
	public int moduleId;
	public ModuleType moduleType;
	public Action OnRoomOpen = () => { };
	public bool isOpen;

	void Start()
	{
		DungeonMgr.instance?.AddModule(moduleId, gameObject);

		Invoke(nameof(CheckDoors), 1f);
	}

	void CheckDoors()
	{
		Vector3 origin = transform.position + Vector3.up;
		Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

		foreach (var direction in directions)
		{
			if (Physics.Raycast(origin, direction, out RaycastHit hit, 5f))
			{
				if (hit.collider.CompareTag("Door"))
				{
					hit.collider.transform.root.GetComponent<DoorCtrl>().OnDoorOpen += OnRoomOpen;
					//hit.collider.transform.root.GetComponent<DoorCtrl>().OnDoorOpen += DoRPC;
				}
			}
		}
	}

	public void DoRPC()
	{
		DungeonMgr.instance.pv.RPC(nameof(Test), RpcTarget.AllBuffered);
	}

	[PunRPC]
	public void Test()
	{
		Debug.Log("문열림" + gameObject.name);
		isOpen = true;
	}
}
