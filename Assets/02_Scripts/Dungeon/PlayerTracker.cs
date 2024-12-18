using Photon.Pun;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	public PlayerPositionTest pp;
	[SerializeField] GameObject model;
	[SerializeField] Transform head;
	[SerializeField] Transform rightHand;
	[SerializeField] Transform leftHand;

	[SerializeField] PhotonView pv;

	private void Start()
	{
		if (pv.IsMine)
		{
			model.SetActive(false);
		}
	}

	private void Update()
	{
		if (pp != null)
		{
			head.position = pp.mc.position;
			Quaternion rotation = pp.mc.rotation;
			rotation.x = 0;
			rotation.z = 0;
			head.rotation = rotation;

			rightHand.position = pp.rc.position;
			rightHand.rotation = pp.rc.rotation;

			leftHand.position = pp.lc.position;
			leftHand.rotation = pp.lc.rotation;
		}
	}
}
