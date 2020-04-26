using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseBalleUp : PowerUp
{
    public float VitesseMultiplicateur = 0f;

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
        GameManager.Instance.ball.currentSpeed *= VitesseMultiplicateur;
    }

    public override void RevertEffect()
    {
        GameManager.Instance.ball.currentSpeed /= VitesseMultiplicateur;
    }
}
