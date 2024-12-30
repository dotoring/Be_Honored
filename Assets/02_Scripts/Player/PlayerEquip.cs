using UnityEngine;

public struct PlayerEquipMent
{
	public EQUIPSTAT Head;
	public EQUIPSTAT Body;
	public EQUIPSTAT Leg;
	public EQUIPSTAT Arm;


}
public partial class Player : MonoBehaviour
{

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

