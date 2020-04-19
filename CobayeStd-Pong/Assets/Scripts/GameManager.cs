using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Baballe ball;
    public Player pong;
    public Player ping;
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
        PauseScreenManager.Instance.IsActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NewBall()
    {
        pong.InitPosition();
        ping.InitPosition();
        ball.Init();
    }

    public void CheckScore(string player, int scoreToCheck)
    {
        if (scoreToCheck >= GameManager.Instance.nbGoalToVictory)
        {
            PauseScreenManager.Instance.Victory(player, pong.Score, ping.Score);
        }
        else
        {
            NewBall();
        }        
    }
    
    public void NewGame()
    {
        pong.Init();
        ping.Init();
        ball.Init();
    }

    

}
