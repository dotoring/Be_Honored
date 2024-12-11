using UnityEngine;

[CreateAssetMenu(fileName = "Equip", menuName = "Game Data/Equip")]
public class Equipment : ScriptableObject
{
	public int hp;
	public int mp;
	public float movespeed;
	public int str;
	public int dex;
	public int intelli;
}
