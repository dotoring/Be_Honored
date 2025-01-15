
using System;
using NUnit.Framework.Internal.Commands;
using UnityEngine;
using UnityEngine.UI;
public class buttonABC : MonoBehaviour
{
    private testA testabb;
    private void OnEnable()
    {
       // testabb.buttonA = this;
        testabb.K = 1;
    }
}



public class testA : MonoBehaviour
{
    private buttonABC buttonA;
    private buttonABC buttonb;
    private buttonABC buttonc;
    private int k;

    public int K
    {
        get { return k; }
        set
        {
            k += value;
            if (k == 3)
            {
                Doit();
            }
        }
    }

    private void Doit()
    {
        // buttonA~~
        // buttonA~~
        // buttonA~~
    }
}
