using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorButton : MonoBehaviour
{
	[SerializeField] XRSimpleInteractable interactable;
	[SerializeField] DoorCtrl doorCtrl;

    void Start()
    {
		interactable.selectEntered.AddListener((_) => doorCtrl.OpenDoor());
    }
}
