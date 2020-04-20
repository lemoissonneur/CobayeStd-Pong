using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baballe : MonoBehaviour
{
    public float initialSpeed = 7f;
    public int start_delay_sec = 2;
    public float reboundAcceleration = 0.7f;
    public float limitSpeed = 20f;

    AudioSource audioSource;
    public AudioClip pingClip;
    public AudioClip pongClip;
    public AudioClip wallClip;

    private bool started = false;
    private float startTime = 0;
    private float currentSpeed;
    private Rigidbody2D rb2D;
    private string lastPlayer = "Player 1";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        startTime = Time.time + start_delay_sec;
    }

    // Update is called once per frame
    void Update()
    {
        if(!started && Time.time > startTime)
        {
            started = true;

            // Lancement de la balle
            rb2D.velocity = Vector2.right * initialSpeed;
            currentSpeed = initialSpeed;
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "Ping":
                {
                    audioSource.PlayOneShot(pingClip, 1f);

                    float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);
                    Vector2 dir = new Vector2(1, y).normalized;
                    currentSpeed = (currentSpeed + reboundAcceleration) > limitSpeed ? limitSpeed : (currentSpeed + reboundAcceleration);
                    rb2D.velocity = dir * currentSpeed;

                    lastPlayer = other.gameObject.tag;
                }
                break;

            case "Pong":
                {
                    audioSource.PlayOneShot(pongClip, 1f);

                    float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);
                    Vector2 dir = new Vector2(-1, y).normalized;
                    currentSpeed = (currentSpeed + reboundAcceleration) > limitSpeed ? limitSpeed : (currentSpeed + reboundAcceleration);
                    rb2D.velocity = dir * currentSpeed;

                    lastPlayer = other.gameObject.tag;
                }
                break;

            default:
                audioSource.PlayOneShot(wallClip, 1f);
                break;
        }
    }

    private float HitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    
    public string LastPlayerTouch()
    {
        return lastPlayer;
    }

    public void Init()
    {
        started = false;
        startTime = Time.time + start_delay_sec;
        transform.position = Vector2.zero;
        rb2D.velocity = Vector2.zero;
    }
}
