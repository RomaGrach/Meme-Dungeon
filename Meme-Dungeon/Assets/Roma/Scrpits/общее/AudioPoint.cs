using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoint : Sounds
{
    //bool NotPlay = true;
    public int nomberOfSound = 0;
    public bool RandomSound = false;
    public float volume = 1f;
    public bool destroyed = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (NotPlay)
        //{
            //NotPlay = false;
            PlaySound(nomberOfSound, volume, RandomSound, destroyed);
            Destroy(gameObject);
        //}
    }
}
