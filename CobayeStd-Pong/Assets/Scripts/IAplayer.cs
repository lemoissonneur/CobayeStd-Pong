using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAplayer : MonoBehaviour
{
    public float speed = 6.5f;
    public enum IANiveau {STUPIDE, PETE};
    public IANiveau niveau;

    private GameObject balle;               // used to decide movement
    private Vector3 ballePosition;          // balle current position
    private Vector3 ballePreviousPosition;  // balle previous position
    private Vector3 balleDirection;         // balle direction

    private float offset;

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private SpriteRenderer spriteRender;
    private Player player;

    void Awake()
    {
        rb2D = this.gameObject.GetComponent<Rigidbody2D>();
        bc2D = this.gameObject.GetComponent<BoxCollider2D>();
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        player = this.gameObject.GetComponent<Player>();

        bc2D.size = spriteRender.bounds.extents * 2;

        balle = GameObject.Find("Baballe");
        ballePosition = balle.transform.position;
        ballePreviousPosition = ballePosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (niveau)
        {
            case IANiveau.STUPIDE:
                IAstupideUpdate();
                break;

            case IANiveau.PETE:
                IApeteUpdate();
                break;
        }
    }

    private void IApeteUpdate()
    {
        // on récup la position actuelle de la balle
        ballePosition = balle.transform.position;

        // on recupère la direction de la balle
        balleDirection = ballePosition - ballePreviousPosition;

        // 
        offset = spriteRender.bounds.extents.y - 10f;

        // si elle va vers le bas, on touche avec le bas de la hitbox, sinon avec le haut
        if (balleDirection.y < 0)
            ballePosition.y -= offset;
        else ballePosition.y += offset;

        if (ballePosition.y > transform.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (ballePosition.y < transform.position.y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    private void IAstupideUpdate()
    {
        ballePosition = balle.GetComponent<Transform>().position;

        if (ballePosition.y > transform.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (ballePosition.y < transform.position.y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}
