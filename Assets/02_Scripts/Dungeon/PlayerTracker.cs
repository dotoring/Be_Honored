using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
	public PlayerPositionTest pp;
	[SerializeField] GameObject model;
	[SerializeField] Transform head;
	[SerializeField] Transform rightHand;
	[SerializeField] Transform leftHand;

	[SerializeField] PhotonView pv;


	[SerializeField] Transform xrori;

	[SerializeField] float yPos;

	private void Start()
	{
		if (pv.IsMine)
		{
			model.SetActive(false);
			xrori=pp.mc.root;
		}
	}

	private void Update()
	{
		if (!pv.IsMine)
			return;
		if (pp != null)
		{
			head.position = pp.mc.position;
			head.position = new Vector3(head.position.x,xrori.position.y+yPos, head.position.z);
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
