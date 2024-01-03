using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deferdfwsf : MonoBehaviour
{
    public GameObject im;
    private void Start()
    {
        Debug.Log("yhyy");
    }
    private void OnTriggerEnter(Collider other)
    {
        im.SetActive(true);




    }
}
