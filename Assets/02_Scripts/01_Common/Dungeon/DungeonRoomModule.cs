using System;
using UnityEngine;

[Serializable]
public class DungeonRoomModule
{
	public int x;
	public int y;

	public bool isExcepted;
	public bool isVisited;
	public DungeonRoomModule(int x, int y)
	{
		this.x = x;
		this.y = y;

		isExcepted = false;
	}

}
