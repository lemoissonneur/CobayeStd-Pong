using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseBalleDown : PowerUp
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
        GameManager.Instance.ball.currentSpeed -= ReductionVitesse;
    }
}
