 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // unity UI available fields
    [SerializeField]
    private float speed = 650f;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    [SerializeField]
    private KeyCode upRightKey = KeyCode.None;
    [SerializeField]
    private KeyCode downLeftKey = KeyCode.None;
    [SerializeField]
    private Text scoreText = null;

    private float limitePos;
    public float LimitePos
    {
        get => limitePos;
    }

    private int score = 0;
    public int Score
    {
        get => score;
        set => score = value;
    }

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private SpriteRenderer spriteRender;
    private Vector2 initPos;

    void Awake()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        bc2D = this.gameObject.GetComponent<BoxCollider2D>();
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();

        bc2D.size = spriteRender.bounds.extents * 2;
    }

    void Update()
    {
        if (Input.GetKey(upRightKey))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(downLeftKey))
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        LimitePosition();
    }

    public void InitPosition()
    {
        transform.position = initPos;
    }

    public void Init(Vector2 position)
    {
        processLimitePos();

        transform.position = initPos = position;
        score = 0;
        scoreText.text = "0";
    }

    public void GoalPlayer()
    {
        score++;
        scoreText.text = score.ToString();

        GameManager.Instance.CheckScore(gameObject.name, score);
    }

    public void processLimitePos()
    {
        limitePos = (TerrainMaker.TargetAreaSizePix.y / 2) - (spriteRender.bounds.extents.y);
        Debug.Log("t="+TerrainMaker.TargetAreaSizePix.y +" s="+ transform.localScale.y +" b="+ spriteRender.bounds.extents.y +" l="+limitePos);
    }

    public void LimitePosition()
    {
        if (transform.position.y > limitePos)
            transform.position = new Vector3(transform.position.x, limitePos, transform.position.z);
        if (transform.position.y < -limitePos)
            transform.position = new Vector3(transform.position.x, -limitePos, transform.position.z);
    }

}
