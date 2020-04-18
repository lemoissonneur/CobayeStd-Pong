using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGoal : MonoBehaviour
{
    Baballe ball;


    void Start()
    {
        ball = GameObject.FindWithTag("Ball").GetComponent<Baballe>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == ball.gameObject.name)
        {
            // Get the play who make this goal
            ScorePlayerManager scorePlayer = GameObject.FindWithTag( ball.LastPlayerTouch() ).GetComponent<ScorePlayerManager>();

            scorePlayer.GoalPlayer();            
        }
    }
}
