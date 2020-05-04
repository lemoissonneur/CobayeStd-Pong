using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Baballe : MonoBehaviour
{
    public float initialSpeed = 700f;
    public int start_delay_sec = 2;
    public float reboundAcceleration = 70f;
    public float finalSpeed = 2000f;
    public float currentSpeed;

    AudioSource audioSource;
    public AudioClip wallClip;

    private bool started = false;
    private float startTime = 0;
    private Rigidbody2D rb2D;
    private CircleCollider2D cc2D;
    private SpriteRenderer spriteRender;
    private string lastPlayer = "Player 1";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = this.gameObject.GetComponent<CircleCollider2D>();
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        cc2D.radius = spriteRender.bounds.extents.x;
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

        audioSource.PlayOneShot(wallClip, 1f);

        if (other.gameObject.name == "Ping" || other.gameObject.name == "Pong")
        {
            float y = HitFactor(transform.position, other.transform.position, other.collider.bounds.size.y);
            float x = (this.transform.position.x > 0) ? -1 : 1;

            Vector2 dir = new Vector2(x, y).normalized;
            //Debug.Log(other.gameObject.name+dir.ToString());

            currentSpeed = currentSpeed < finalSpeed ? (currentSpeed+reboundAcceleration> finalSpeed ? finalSpeed : currentSpeed + reboundAcceleration) : currentSpeed;

            rb2D.velocity = dir * currentSpeed;

            lastPlayer = other.gameObject.tag;
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
