using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PlayerEquipMent
{
	public EQUIPSTAT Head;
	public EQUIPSTAT Body;
	public EQUIPSTAT Leg;
	public EQUIPSTAT Arm;


}
public partial class Player
{
	internal List<GameObject> Attackon;

	public void ArmorChange(EquipType equipType, EQUIPSTAT stat)
	{
		switch (equipType)
		{
			case EquipType.HEAD:
				_armor.Head = stat;
				break;
			case EquipType.BODY:
				_armor.Body = stat;
				break;
			case EquipType.LEG:
				_armor.Leg = stat;
				break;
			case EquipType.ARM:
				_armor.Arm = stat;
				break;

			default:
				break;
		}

	}
}

