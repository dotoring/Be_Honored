using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollision : MonoBehaviour
{
    public GameObject crackPrefab;
    public Vector3 crackOffset;

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
            ContactPoint contact = collision.contacts[0];
            Vector3 point = contact.point;

            // Calculate the rotation from the contact point
            Vector3 normal = contact.normal;
            Quaternion rotation = Quaternion.LookRotation(-normal, Vector3.up);

            // Instantiate the crack prefab at the contact point with the calculated rotation
            GameObject crack = Instantiate(crackPrefab, point, rotation);

            // Set the position of the crack object relative to the shield
            crack.transform.SetParent(transform);
            crack.transform.localPosition = crackOffset;

            Destroy(crack, 2f);
        }        
    }
}
