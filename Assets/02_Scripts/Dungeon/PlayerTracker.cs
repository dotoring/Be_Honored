using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	public PlayerPositionTest pp;
	[SerializeField] Transform head;
	[SerializeField] Transform rightHand;
	[SerializeField] Transform leftHand;

	private void Update()
	{
		if (pp != null)
		{
			head.position = pp.mc.position;

			rightHand.position = pp.rc.position;
			rightHand.rotation = pp.rc.rotation;

			leftHand.position = pp.lc.position;
			leftHand.rotation = pp.lc.rotation;
		}
	}
}
