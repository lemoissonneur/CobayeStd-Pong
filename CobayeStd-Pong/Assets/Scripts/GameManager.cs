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


    void Start()
    {
        PauseScreenManager.Instance.IsActive(false);
        TerrainMaker.Instance.MakeTerrain();
        NewGame();
    }

    void Update()
    {
        
    }



    /**************************************************************************
    *                                 PUBLIC
    /**************************************************************************/
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

    private void NewBall()
    {
        pong.InitPosition();
        ping.InitPosition();
        ball.Init();
    }

    public void NewGame()
    {
        pong.Init( new Vector2(TerrainMaker.topRight.x - 1, 0) );
        ping.Init( new Vector2(TerrainMaker.bottomLeft.x + 1, 0) );
        ball.Init();
    }




    /**************************************************************************
    *                                 PRIVATE
    /**************************************************************************/
    private void MakeTerrain()
    {

    }


}
