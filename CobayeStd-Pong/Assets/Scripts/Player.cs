 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6.5f;
    public KeyCode upRightKey = KeyCode.None;
    public KeyCode downLeftKey = KeyCode.None;

    public float LimitePos = 4.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(upRightKey) && transform.position.y < LimitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(downLeftKey) && transform.position.y > -LimitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

}
