using NUnit.Framework;
using UnityEngine;

public abstract class Factory : MonoBehaviour
{
	public abstract GameObject GetPref(int type);
}
