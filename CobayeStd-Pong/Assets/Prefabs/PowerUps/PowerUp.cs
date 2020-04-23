using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public abstract class PowerUp : MonoBehaviour
{
    //public abstract float probability { get; }
    private GameObject balle;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ping");
    }

    public abstract void Effect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PowerUp triggered by "+ collision.gameObject.tag);
        if(collision.gameObject.tag == "Ball")
        {
            Debug.Log("Effect");
            Effect();
            Destroy(this.gameObject);
        }
    }
}