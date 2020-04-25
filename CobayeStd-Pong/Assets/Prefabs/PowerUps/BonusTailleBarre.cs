using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTailleBarre : PowerUp
{
    public float AugmentationTaille = 0f;
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
            target = GameManager.Instance.pong;
        else if (GameManager.Instance.ball.LastPlayerTouch() == "Player 2")
            target = GameManager.Instance.ping;

        target.transform.localScale = new Vector3(
                GameManager.Instance.pong.transform.localScale.x,
                GameManager.Instance.pong.transform.localScale.y * AugmentationTaille,
                GameManager.Instance.pong.transform.localScale.z);
        target.processLimitePos();
    }

    public override void RevertEffect()
    {
        target.transform.localScale = new Vector3(
                GameManager.Instance.pong.transform.localScale.x,
                GameManager.Instance.pong.transform.localScale.y / AugmentationTaille,
                GameManager.Instance.pong.transform.localScale.z);
        target.processLimitePos();
    }
}
