using System.Collections.Generic;
using UnityEngine;

public class ScrapFactory : Factory
{
	[SerializeField] List<GameObject> scraps;

	public override GameObject GetPref(int type)
	{
		try
		{
			return scraps[type];
		}
		catch
		{
			Debug.LogWarning("Wrong number of scrap type");
			return null;
		}
	}
}
