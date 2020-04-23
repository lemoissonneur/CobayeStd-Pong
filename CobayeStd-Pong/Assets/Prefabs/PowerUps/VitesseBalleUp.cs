using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitesseBalleUp : PowerUp
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
        GameManager.Instance.ball.currentSpeed += AugmentationVitesse;
    }
}
