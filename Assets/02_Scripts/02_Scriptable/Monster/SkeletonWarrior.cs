using UnityEngine;

[CreateAssetMenu(fileName = "SkeletonWarrior", menuName = "Scriptable Objects/SkeletonWarrior")]
public class SkeletonWarrior : ScriptableObject
{
	public const string PLAYER_TAG = "Player";
	public float detectRange = 10.0f;
	public float attackRange = 2.0f;
	public float attackPower = 1.0f;
	public float hp = 10.0f;
}
