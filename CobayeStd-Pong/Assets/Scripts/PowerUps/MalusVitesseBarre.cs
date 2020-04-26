using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusVitesseBarre : PowerUp
{
    public float VitesseDiviseur = 0f;
    private Player target;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    public override void ApplyEffect()
    {
        if (GameManager.Instance.ball.LastPlayerTouch() == "Player 1")
            target = GameManager.Instance.ping;
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            target = GameManager.Instance.pong;

            target.Speed /= VitesseDiviseur;
    }

    public override void RevertEffect()
    {
        target.Speed *= VitesseDiviseur;
    }
}
