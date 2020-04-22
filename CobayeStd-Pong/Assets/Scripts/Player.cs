 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 6.5f;
    [SerializeField]
    private KeyCode upRightKey = KeyCode.None;
    [SerializeField]
    private KeyCode downLeftKey = KeyCode.None;
    [SerializeField]
    private Text scoreText;


    private float limitePos = 4.2f;
    

    private int score = 0;
    public int Score
    {
        get => score;
        set => score = value;
    }

    private Vector2 initPos;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        limitePos = (TerrainMaker.TerrainSize.y / 2) - (TerrainMaker.BarreSize.y / 2);

        if (Input.GetKey(upRightKey) && transform.position.y < limitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(downLeftKey) && transform.position.y > -limitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }


    public void InitPosition()
    {
        transform.position = initPos;
    }

    public void Init(Vector2 position)
    {
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

}
