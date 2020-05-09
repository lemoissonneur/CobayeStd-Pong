using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Baballe ball;
    public Player pong;
    public Player ping;
    public PowerUpGenerator PUGenerator;
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


    void Start()
    {
        PauseScreenManager.Instance.IsActive(false);
        TerrainMaker.Instance.MakeTerrain();
        NewGame();
    }

    void Update()
    {
        CheckBallPosition();
    }



    /**************************************************************************
    *                                 PUBLIC
    /**************************************************************************/
    public void CheckScore(string player, int scoreToCheck)
    {
        if (scoreToCheck >= GameManager.Instance.nbGoalToVictory)
        {
            PUGenerator.StopGenerator();
            PauseScreenManager.Instance.Victory(player, pong.Score, ping.Score);
        }
        else
        {
            NewBall();
        }
    }

    private void NewBall()
    {
        pong.InitPosition();
        ping.InitPosition();
        ball.Init();
        PUGenerator.StartGenerator();
    }

    public void NewGame()
    {
        pong.Init(TerrainMaker.PongPosition);
        ping.Init(TerrainMaker.PingPosition);
        ball.Init();
        PUGenerator.StartGenerator();
    }




    /**************************************************************************
    *                                 PRIVATE
    /**************************************************************************/
    private void CheckBallPosition()
    {
        if (Vector2.SqrMagnitude(ball.transform.position) > Vector2.SqrMagnitude(TerrainMaker.UsedScreenSizePix) * 2)
        {
            ball.transform.position = Vector2.zero;
        }
    }


}
