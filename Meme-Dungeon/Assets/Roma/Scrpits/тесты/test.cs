using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : Sounds
{
    public GameObject im;
    
    private void OnTriggerEnter(Collider other)
    {
        im.SetActive(true);
        //PlaySound(0, 1, true,false);


    }
}
