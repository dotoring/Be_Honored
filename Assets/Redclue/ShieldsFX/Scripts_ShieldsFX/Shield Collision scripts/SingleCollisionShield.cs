using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCollisionShield : MonoBehaviour
{
    public GameObject crackPrefab;
    public Vector3 crackOffset;
    public float crackDestroyTime = 1f;

    private GameObject currentCrack;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (currentCrack != null)
            {
                // Destroy the current crack prefab
                Destroy(currentCrack);
            }

            ContactPoint contact = collision.contacts[0];
            Vector3 point = contact.point;

            // Calculate the rotation from the contact point
            Vector3 normal = contact.normal;
            Quaternion rotation = Quaternion.LookRotation(-normal, Vector3.up);

            // Instantiate the new crack prefab at the contact point with the calculated rotation
            currentCrack = Instantiate(crackPrefab, point, rotation);

            // Set the position of the crack object relative to the shield
            currentCrack.transform.SetParent(transform);
            currentCrack.transform.localPosition = crackOffset;

            Destroy(currentCrack, crackDestroyTime);
        }
    }
        
}

