 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    public enum deplacementMode {HORIZONTAL, VERTICAL};
    public deplacementMode deplacement = deplacementMode.HORIZONTAL;
    public KeyCode upKey = KeyCode.None;
    public KeyCode downKey = KeyCode.None;

    public float verticalLimitePos = 4.2f;
    public float horizontalLimitePos = 8.1f;
    public float limite = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deplacement == deplacementMode.HORIZONTAL)
            deplacementHorizontal();
        else if (deplacement == deplacementMode.VERTICAL)
            deplacementVertical();
    }

    void deplacementVertical()
    {
        if (Input.GetKey(upKey) && transform.position.y < verticalLimitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(downKey) && transform.position.y > -verticalLimitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    void deplacementHorizontal()
    {
        if (Input.GetKey(upKey) && transform.position.x < horizontalLimitePos)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(downKey) && transform.position.x > -horizontalLimitePos)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
