using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float delay = 0.3f;
    public float KillDelay = 0.2f;
    public float damage = 1f;
    public AudioSource hurtS;
    public AudioSource reflS;
    private float timeSinceHit;
    private Collider Player;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider" + other.name);
        try { 
            if (other.GetComponent<Health>().EnemyTags.Contains(gameObject.tag) && other.tag == "Player")
            {
                Hurt(other);
            }
        }
        catch {}
    }
    private void OnTriggerStay(Collider other)
    {
        try
        {
            if (other.GetComponent<Health>().EnemyTags.Contains(gameObject.tag) && other.tag == "Player")
            {
                Hurt(other);
            }
        }
        catch {}
    }
    private void Hurt(Collider other)
    {
        if (Time.time - timeSinceHit >= delay)
        {
            if (other.GetComponent<Health>().PoweredUp)
            {
                Debug.Log("Deflected the Attack");
                reflS.Play();
                Invoke("Die", KillDelay);
            }
            else { 
                hurtS.Play();
                if (other.GetComponent<Health>().hp <= damage) 
                {
                    Player = other;
                    Invoke("Kill", KillDelay);
                }
                other.GetComponent<Health>().hp -= damage;
                Debug.Log("Player Received Damage");
            }
            timeSinceHit = Time.time;
        }

    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void Kill()
    {
        Destroy(Player.gameObject);
    }
}

