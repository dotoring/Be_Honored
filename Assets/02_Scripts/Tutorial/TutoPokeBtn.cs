using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TutoPokeBtn : MonoBehaviour
{
    [SerializeField] XRSimpleInteractable interactable;
    [SerializeField] TutoDoor doorCtrl;

    void Start()
    {
        interactable.selectEntered.AddListener((_) => doorCtrl.OpenDoor());
    }
}
