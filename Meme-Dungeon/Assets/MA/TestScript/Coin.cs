using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotSpeed = 1f;
    public float delay = 0.3f;
    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Transform>().Rotate(rotSpeed, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin interacted");
        if (other.tag == "Player")
        {
            try { GetComponent<AudioSource>().Play(); }
            catch { }
            Invoke("Pickup", delay);
        }
    }
    private void Pickup()
    {
        Destroy(gameObject);
        Debug.Log("Picked Up Coin");
    }
}
