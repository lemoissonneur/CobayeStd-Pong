using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTailleBarre : PowerUp
{
    public float AugmentationTaille = 0f;

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
            GameManager.Instance.pong.transform.localScale = new Vector3(GameManager.Instance.pong.transform.localScale.x, GameManager.Instance.pong.transform.localScale.y * AugmentationTaille, 1);
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            GameManager.Instance.ping.transform.localScale = new Vector3(GameManager.Instance.ping.transform.localScale.x, GameManager.Instance.ping.transform.localScale.y * AugmentationTaille, 1);
    }
}
