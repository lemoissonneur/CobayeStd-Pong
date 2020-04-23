using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVitesseBarre : PowerUp
{
    public float AugmentationVitesse = 0f;

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
            GameManager.Instance.pong.speed += AugmentationVitesse;
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            GameManager.Instance.ping.speed += AugmentationVitesse;
    }
}
