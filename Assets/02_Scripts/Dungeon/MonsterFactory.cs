using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonsterFactory : Factory
{
	public List<GameObject> monsters = new List<GameObject>();

	public override GameObject GetPref(int type)
	{
		try
		{
			return monsters[type];
		}
		catch
		{
			Debug.LogWarning("Wrong number of monster type");
			return null;
		}
	}

	public override GameObject SpawnObejct(string name, Vector3 position, int id)
	{
		GameObject go = PhotonNetwork.InstantiateRoomObject(name, position, Quaternion.identity);
		go.GetComponent<PhotonView>().RPC("SetId", RpcTarget.AllBuffered, id);
		return go;
	}
}
