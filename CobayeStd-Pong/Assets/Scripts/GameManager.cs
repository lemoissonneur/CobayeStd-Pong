using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Baballe ball;
    public Player Pong;
    public Player Ping;
    public int nbGoalToVictory = 5;



    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Victory()
    {

    }

    public void Goal()
    {
        ball.ReLaunch(Vector2.right);
    }

    public void ReStartGameBtn()
    {
        ball.ReLaunch(Vector2.right);
    }

    public void ReturnMainMenuBtn()
    {
        SceneManager.LoadScene("Accueil");
    }

}
