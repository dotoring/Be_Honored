using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VendorPokeButton : MonoBehaviour
{
	[SerializeField] XRSimpleInteractable interactable;
	[SerializeField] VendorUI vendor;

	private void Start()
	{
		interactable.selectEntered.AddListener(_ =>
		{
			vendor.SellItem();
		});
	}
}
