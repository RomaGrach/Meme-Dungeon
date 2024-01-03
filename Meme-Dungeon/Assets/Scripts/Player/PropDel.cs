using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class PropDel : MonoBehaviour
{
    public KeyCode DeleteKey = KeyCode.Mouse0;

    void Start()
    {
        
    }
    void Update()
    {
    }
    private void OnTriggerStay(Collider other) {
        if (Input.GetKey(DeleteKey)) {
            Destroy(other.gameObject);
            Debug.Log("Deleted" + other.name);
        }
    }
}
