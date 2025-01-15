using System;
using UnityEngine;

public class TutoBagPortal : MonoBehaviour
{
    [SerializeField] GameObject bagPref;
    public Action opend;

    private void Start()
    {
        opend += () =>
        {
            Debug.Log("Bag is Opend");
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BagSensor"))
        {
            if (BagCtrl.Instance == null)
            {
                Instantiate(bagPref, transform.position, Quaternion.identity);
            }
            else
            {
                BagCtrl.Instance.gameObject.transform.position = transform.position;
            }

            opend?.Invoke();
        }
    }
}