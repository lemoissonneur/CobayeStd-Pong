using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseBalleDown : PowerUp
{
    public float ReductionVitesse = 0f;

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
        GameManager.Instance.ball.currentSpeed /= ReductionVitesse;
    }

    public override void RevertEffect()
    {
        GameManager.Instance.ball.currentSpeed *= ReductionVitesse;
    }
}
