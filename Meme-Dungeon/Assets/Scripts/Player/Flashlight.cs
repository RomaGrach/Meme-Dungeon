using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public KeyCode FlashLightKey = KeyCode.F;

    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(FlashLightKey))
        {
            GetComponentInChildren<Light>().enabled = !GetComponentInChildren<Light>().enabled;
        }
    }
}
