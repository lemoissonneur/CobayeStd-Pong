using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baballe : MonoBehaviour
{
    public float initialSpeed = 1f;
    public int start_delay_sec = 3;

    AudioSource audioSource;
    public AudioClip pingClip;
    public AudioClip pongClip;
    public AudioClip wallClip;

    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!started && Time.time > start_delay_sec)
        {
            started = true;
            // lancement de la balle ici
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Ping":
                Debug.Log("ping");
                audioSource.PlayOneShot(pingClip, 1f);
                break;

            case "Pong":
                Debug.Log("pong");
                audioSource.PlayOneShot(pongClip, 1f);
                break;

            case "TopWall":
                Debug.Log("top");
                audioSource.PlayOneShot(wallClip, 1f);
                break;

            case "BottomWall":
                Debug.Log("bottom");
                audioSource.PlayOneShot(wallClip, 1f);
                break;

            case "LeftWall":
            case "RightWall":
                SceneManager.LoadScene("Accueil");
                break;
        }
    }
}
