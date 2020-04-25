using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVitesseBarre : PowerUp
{
    public float AugmentationVitesse = 0f;
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

        target.Speed *= AugmentationVitesse;
    }

    public override void RevertEffect()
    {
        target.Speed /= AugmentationVitesse;
    }
}
