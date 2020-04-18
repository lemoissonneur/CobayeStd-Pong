using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAstupide : MonoBehaviour
{
    public float speed = 6.5f;
    public float LimitePos = 4.2f;
    private GameObject balle;
    private Transform balleTransform;
    //private Vector3 ballePreviousPosition;
    //private Vector3 balleDirection;

    // Start is called before the first frame update
    void Start()
    {
        balle = GameObject.Find("Baballe");
        balleTransform = balle.GetComponent<Transform>();
        //ballePreviousPosition = balleTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        balleTransform = balle.GetComponent<Transform>();

        if (balleTransform.position.y > transform.position.y && transform.position.y < LimitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (balleTransform.position.y < transform.position.y && transform.position.y > -LimitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

    }
}
