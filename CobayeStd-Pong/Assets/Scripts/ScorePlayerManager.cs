using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePlayerManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score = 0;
    public int Score
    {
        get => score;
        set => score = value;
    }
    

    void Start()
    {
        InitScore();
    }

    public void InitScore()
    {
        scoreText.text = "0";
    }

    public void GoalPlayer()
    {        
        score++;
        scoreText.text = score.ToString();

        if (score >= GameManager.Instance.nbGoalToVictory)
        {
            GameManager.Instance.Victory();
        }
        else
        {
            GameManager.Instance.Goal();
        }
    }

}
