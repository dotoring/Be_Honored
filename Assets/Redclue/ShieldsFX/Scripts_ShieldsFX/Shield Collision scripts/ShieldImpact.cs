using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldImpact : MonoBehaviour
{
    public GameObject impactVfx;
    public float waitBeforeDestroyImpact = 1f;

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
            var impact = Instantiate(impactVfx, transform);
            Destroy(impact, waitBeforeDestroyImpact);
        }
    }
}
