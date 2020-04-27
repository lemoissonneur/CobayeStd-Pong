using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusTailleBarre : PowerUp
{
    public float TailleDiviseur = 0f;
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

        target.transform.localScale = new Vector3(
                target.transform.localScale.x,
                target.transform.localScale.y / TailleDiviseur,
                target.transform.localScale.z);
        target.processLimitePos();
    }

    public override void RevertEffect()
    {
        target.transform.localScale = new Vector3(
                target.transform.localScale.x,
                target.transform.localScale.y * TailleDiviseur,
                target.transform.localScale.z);
        target.processLimitePos();
    }
}
