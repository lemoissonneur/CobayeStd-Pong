using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusTailleBarre : PowerUp
{
    public float ReductionTaille = 0f;

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
            GameManager.Instance.ping.transform.localScale = new Vector3(GameManager.Instance.ping.transform.localScale.x, GameManager.Instance.ping.transform.localScale.y * ReductionTaille, 1);
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            GameManager.Instance.pong.transform.localScale = new Vector3(GameManager.Instance.pong.transform.localScale.x, GameManager.Instance.pong.transform.localScale.y * ReductionTaille, 1);
    }
}
