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

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Ping":
                {
                    Debug.Log("ping");
                    audioSource.PlayOneShot(pingClip, 1f);

                    float y = HitFactor(transform.position, other.transform.position, other.bounds.size.y);
                    Vector2 dir = new Vector2(1, y).normalized;
                    currentSpeed = (currentSpeed + reboundAcceleration) > limitSpeed ? limitSpeed : (currentSpeed + reboundAcceleration);
                    rb2D.velocity = dir * currentSpeed;

                    lastPlayer = other.gameObject.tag;
                }
                break;

            case "Pong":
                {
                    Debug.Log("pong");
                    audioSource.PlayOneShot(pongClip, 1f);

                    float y = HitFactor(transform.position, other.transform.position, other.bounds.size.y);
                    Vector2 dir = new Vector2(-1, y).normalized;
                    currentSpeed = (currentSpeed + reboundAcceleration) > limitSpeed ? limitSpeed : (currentSpeed + reboundAcceleration);
                    rb2D.velocity = dir * currentSpeed;

                    lastPlayer = other.gameObject.tag;
                }
                break;

            case "TopWall":
                {
                    Debug.Log("top");
                    audioSource.PlayOneShot(wallClip, 1f);

                    Vector2 inDirection = rb2D.velocity;
                    Vector2 inNormal = Vector2.down;
                    Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
                    rb2D.velocity = newVelocity;
                }
                break;

            case "BottomWall":
                {
                    Debug.Log("bottom");
                    audioSource.PlayOneShot(wallClip, 1f);

                    Vector2 inDirection = rb2D.velocity;
                    Vector2 inNormal = Vector2.up;
                    Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
                    rb2D.velocity = newVelocity;
                }
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
