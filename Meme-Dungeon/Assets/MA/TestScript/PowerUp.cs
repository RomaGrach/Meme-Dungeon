using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float rotSpeed = 1f;
    public float delay = 0.3f;
    public float PowerUpTime = 5f;
    public string[] PickupTags;
    private Collider Player;
    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Transform>().Rotate(0, rotSpeed, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PowerUp interacted");
        if (PickupTags.Contains(other.tag))
        {
            try { GetComponent<AudioSource>().Play(); }
            catch { }
            Player = other;
            Invoke("Pickup", delay);
        }
    }
    private void Pickup()
    {
        try { Player.GetComponent<Health>().PoweredUp = true;
        }
        catch { }
        Invoke("TurnOff", PowerUpTime);
        Destroy(gameObject.GetComponentInChildren<MeshRenderer>());
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        Debug.Log("Picked Up PowerUp");
    }
    private void TurnOff() {
        try
        {
            Player.GetComponent<Health>().PoweredUp = false;
            Debug.Log("PowerUp Ended");
            Destroy(gameObject);
        }
        catch { }
    }
}
