using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusVitesseBarre : PowerUp
{
    public float ReductionVitesse = 0f;

    // Start is called before the first frame update
    new void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Effect()
    {
        if (GameManager.Instance.ball.LastPlayerTouch() == "Player 1")
            GameManager.Instance.ping.speed -= ReductionVitesse;
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            GameManager.Instance.pong.speed -= ReductionVitesse;
    }
}
