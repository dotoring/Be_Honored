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

	[Header("For BossRoom")]
	[SerializeField] PlayerEnterChecker playerEnterChecker = null;
	[SerializeField] GameObject exitPortal = null;

	void Start()
	{
		DungeonMgr.instance?.AddModule(moduleId, gameObject);

		if(moduleType == ModuleType.Boss)
		{
			playerEnterChecker.OnPlayerEnter += OnRoomOpen;
		}
		else
		{
			Invoke(nameof(CheckDoors), 0.5f);
		}
	}

	void CheckDoors()
	{
		Vector3 origin = transform.position + Vector3.up;
		Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
		Vector3 size = new Vector3(2, 2, 2);
		int layerMask = LayerMask.GetMask("Door");

		foreach (var direction in directions)
		{
			if (Physics.BoxCast(origin, size, direction, out RaycastHit hit, Quaternion.identity, 5f, layerMask))
			{
				if (hit.collider.CompareTag("Door"))
				{
					hit.collider.transform.root.GetComponent<DoorCtrl>().OnDoorOpen += OnRoomOpen;
					Debug.Log(hit.collider.transform.root.GetComponent<DoorCtrl>().isOpen);
					this.isOpen = hit.collider.transform.root.GetComponent<DoorCtrl>().isOpen;
				}
			}
		}
	}

	public void OpenPortal()
	{
		exitPortal.SetActive(true);
	}
}
