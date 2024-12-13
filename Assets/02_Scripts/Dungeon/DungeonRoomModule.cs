using System;
using System.IO;
using UnityEngine;

public enum ModuleType
{
	Start = 0,
	End,
	Monsters,
	Scraps,
	Empty,
}
public class DungeonRoomModule
{
	public int x;
	public int y;

	public bool isExcepted;
	public bool isVisited;

	public ModuleType moduleType;
	public DungeonRoomModule(int x, int y)
	{
		this.x = x;
		this.y = y;

		isExcepted = false;
		isVisited = false;
		moduleType = ModuleType.Empty;
	}

	//포톤에서 사용하기 위한 직렬화
	public static byte[] Serialize(object customType)
	{
		var c = (DungeonRoomModule)customType;

		// MemoryStream에 데이터를 순서대로 씀
		using (MemoryStream ms = new MemoryStream())
		{
			ms.Write(BitConverter.GetBytes(c.x), 0, sizeof(int)); // 4바이트
			ms.Write(BitConverter.GetBytes(c.y), 0, sizeof(int)); // 4바이트
			ms.WriteByte((byte)(c.isExcepted ? 1 : 0));           // 1바이트
			ms.WriteByte((byte)(c.isVisited ? 1 : 0));            // 1바이트
			ms.Write(BitConverter.GetBytes((int)c.moduleType), 0, sizeof(int)); // 4바이트

			// 스트림 데이터를 바이트 배열로 반환
			return ms.ToArray();
		}
	}

	public static object Deserialize(byte[] data)
	{
		DungeonRoomModule module = new DungeonRoomModule(0, 0);

		// 각 데이터의 위치를 계산하여 읽음
		int offset = 0;

		// 정수 x 읽기 (4바이트)
		module.x = BitConverter.ToInt32(data, offset);
		offset += sizeof(int);

		// 정수 y 읽기 (4바이트)
		module.y = BitConverter.ToInt32(data, offset);
		offset += sizeof(int);

		// bool isExcepted 읽기 (1바이트)
		module.isExcepted = data[offset] != 0;
		offset += 1;

		// bool isVisited 읽기 (1바이트)
		module.isVisited = data[offset] != 0;
		offset += 1;

		// 정수 moduleType 읽기 (4바이트)
		module.moduleType = (ModuleType)BitConverter.ToInt32(data, offset);

		return module;
	}
}
