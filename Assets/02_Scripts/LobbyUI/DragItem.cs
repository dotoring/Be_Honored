using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// VendorSellPoint
public class DragItem : MonoBehaviour
{
	[SerializeField] Material matRed;
	[SerializeField] Material matGray;
	[SerializeField] MeshRenderer mrenderer;
	[SerializeField] LayerMask targetlayer;

	public Transform parentToReturnTo = null;
	XRGrabInteractable xRGrabInteractable;
	bool OnSellPoint = false;
	EquipType equipPart;
	RectTransform rectTransform;
	Rigidbody rig;
	[SerializeField] ItemManager itemManager;
	[SerializeField] TMP_Text textOfItemGrab;
	public TMP_Text textOfItemIninven;
	[SerializeField] GameObject grabed;
	[SerializeField] GameObject ininven;

	public void setItem(Transform par, ItemManager manager, string itemName)
	{
		parentToReturnTo = par;
		itemManager = manager;
		textOfItemGrab.text = itemName;
		textOfItemIninven.text = itemName;
	}

	private void Awake()
	{
		rig = GetComponent<Rigidbody>();
		mrenderer = GetComponent<MeshRenderer>();
		xRGrabInteractable = GetComponent<XRGrabInteractable>();
		rectTransform = GetComponent<RectTransform>();
		//parentToReturnTo = transform.parent;
		xRGrabInteractable.selectEntered.AddListener(_ => OnSelectEnter());
		xRGrabInteractable.selectExited.AddListener(_ => OnSelectExit());
	}

	private void OnSelectExit()
	{
		if (OnSellPoint)
		{
			Debug.Log($" selling item");
			// TODO : Add Gold
			Destroy(this.gameObject);
		}
		else
		{
			Debug.Log($"Return Object");
			itemManager.AddItem(this.gameObject);
			rectTransform.localScale = Vector3.one * 0.3f;
			grabed.SetActive(false);
			ininven.SetActive(true);

		}
	}

	void OnSelectEnter()
	{
		//parentToReturnTo = transform.parent;
		itemManager.RemoveItem(this.gameObject);
		grabed.SetActive(true);
		ininven.SetActive(false);

	}
	private void OnTriggerEnter(Collider other)
	{

		if (((1 << other.gameObject.layer) & targetlayer) != 0)
		{
			Debug.Log($" trigger enter ");
			mrenderer.material = matRed;
			OnSellPoint = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (((1 << other.gameObject.layer) & targetlayer) != 0)
		{

			Debug.Log($" trigger exit ");
			mrenderer.material = matGray;
			OnSellPoint = false;
		}
	}


}
