using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsSound : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource Footclip;
    public AudioClip[] FootSteps;
    public float timeBetweenSteps = 0.1f;
    private int k = 0;
    private float interval = 0f;

    // Update is called once per frame
    private void Start()
    {
        Footclip = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (GetComponent<FirstPersonController>().isWalking)
        {
            float t = timeBetweenSteps;
            if (GetComponent<FirstPersonController>().isSprinting) t /= 2;
            if (GetComponent<FirstPersonController>().isCrouched) t *= 2;
            if (Time.time - interval > t)
            {
                PlaySound();
                interval = Time.time;
            }
        }


    }
    void PlaySound()
    {
        if (k > 3) k = 0;
        Footclip.clip = FootSteps[k];
        Footclip.Play();
        k++;
    }
}
