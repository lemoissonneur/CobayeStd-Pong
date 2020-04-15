using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip startClip;
    public AudioClip loopClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(startClip, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
            audioSource.PlayOneShot(loopClip, 1f);
    }
}
