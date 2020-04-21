using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    Baballe ball;


    void Awake()
    {
        ball = GameObject.FindWithTag("Ball").GetComponent<Baballe>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == ball.gameObject.name)
        {
            // Get the play who make this goal
            Player scorePlayer = GameObject.FindWithTag( ball.LastPlayerTouch() ).GetComponent<Player>();

            scorePlayer.GoalPlayer();            
        }
    }
}
