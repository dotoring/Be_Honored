using System;
using System.Collections;
using UnityEngine;

public class TutoDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    public bool isOpen;

    public Action OnDoorOpen;

    public void OpenDoor()
    {
        OpenCoroutine();
    }

    private void OpenCoroutine()
    {
        this.isOpen = true;
        StartCoroutine(RotateDoor());
    }
    
    IEnumerator RotateDoor()
    {
        while(door.transform.localRotation.eulerAngles.y < 89)
        {
            door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, Quaternion.Euler(0, 90, 0), 0.1f);
            yield return new WaitForSeconds(0.02f);
        }
        OnDoorOpen.Invoke();
    }
}
