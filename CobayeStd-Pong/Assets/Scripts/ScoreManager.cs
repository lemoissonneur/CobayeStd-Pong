using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text scorePingText;
    [SerializeField]
    private Text scorePongText;

    private int scorePingValue;
    public int ScorePingValue
    {
        get => scorePingValue;
        set => scorePingValue = value;
    }

    private int scorePongValue;
    public int ScorePongValue
    {
        get => scorePongValue;
        set => scorePongValue = value;
    }


    private static ScoreManager instance;
    public static ScoreManager Instance
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
        InitScore();
    }

    public void InitScore()
    {
        scorePingText.text = "0";
        scorePingValue = 0;

        scorePongText.text = "0";
        scorePongValue = 0;
    }

    public void GoalPing()
    {
        scorePingValue++;
        scorePingText.text = scorePingValue.ToString();
    }

    public void GoalPong()
    {
        scorePongValue++;
        scorePongText.text = scorePongValue.ToString();
    }

}
