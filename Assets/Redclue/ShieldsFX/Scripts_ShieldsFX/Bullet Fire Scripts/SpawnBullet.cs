using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public GameObject firepoint;
    public GameObject fx;
    public RotateGunToMouse rotate;


    private GameObject effectToSpawn;
    private float timeToFire = 0;
    void Start()
    {
        effectToSpawn = fx;
    }


    void Update()
    {
        if (Input.GetMouseButton(1) && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / effectToSpawn.GetComponent<FireBullet>().fireRate;
            SpawnVFX();
        }
        
    }

    void SpawnVFX()
    {
        GameObject fx;

        if (firepoint != null)
        {
            fx = Instantiate(effectToSpawn, firepoint.transform.position, Quaternion.identity);
            if (rotate != null)
            {
                fx.transform.localRotation = rotate.GetRotation();
            }
        }

        else
        {
            Debug.Log("No Fire Point");
        }
    }

    }