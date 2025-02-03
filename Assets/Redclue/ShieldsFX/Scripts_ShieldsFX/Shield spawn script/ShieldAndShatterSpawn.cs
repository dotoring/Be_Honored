using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAndShatterSpawn : MonoBehaviour
{
    public bool death;    
    public float cooldown;
    public GameObject[] shieldvfx;
    public GameObject[] shatterVfx;

    private int number;
    private float timer;
    private bool isPressed;
    private GameObject currentShield;


    void Start()
    {
        Dtimer();
        ChangeCurrent(0);
    }

    
    void Update()
    {
        if (death == true)
        {
            timer += Time.deltaTime;
        }
        
        if (timer >= cooldown)
        {
            shieldvfx[number].transform.position = transform.position;
            currentShield = Instantiate(shieldvfx[number]);
            Dtimer();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            death = true;
            shatterVfx[number].transform.position = transform.position;
            currentShield = Instantiate(shatterVfx[number]);
            Destroy(currentShield, 2f);
        }
    }

    void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isPressed = false;            
        }

        if (!isPressed && Input.GetKeyDown(KeyCode.A))
        {
            isPressed = true;
            ChangeCurrent(-1);
            Dtimer();
        }

        if (!isPressed && Input.GetKeyDown(KeyCode.D))
        {
            isPressed = true;
            ChangeCurrent(+1);
            Dtimer();
        }
    }

    void ChangeCurrent(int delta)
    {
        number += delta;
        if (number > shieldvfx.Length - 1)
            number = 0;
        else if (number < 0)
            number = shieldvfx.Length - 1;

        if (currentShield != null)
        {
            Destroy(currentShield);
        }
        currentShield = Instantiate(shieldvfx[number]);
    }

    void Dtimer()
    {
        death = false;
        timer = 0;
    }


}//class























