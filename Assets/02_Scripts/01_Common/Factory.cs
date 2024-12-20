using NUnit.Framework;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
	public abstract GameObject GetPref(int type);

	public abstract GameObject SpawnObejct(string name, Vector3 position, int id = 0);
}
