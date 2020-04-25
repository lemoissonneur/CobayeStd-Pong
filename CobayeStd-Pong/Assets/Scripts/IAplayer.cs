using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAplayer : MonoBehaviour
{
    public float speed = 6.5f;
    public enum IANiveau {STUPIDE, PETE};
    public IANiveau niveau;
    private GameObject balle;
    private Vector3 ballePosition;
    private Vector3 ballePreviousPosition;
    private Vector3 balleDirection;
    private float offset;

    private Rigidbody2D rb2D;
    private BoxCollider2D bc2D;
    private SpriteRenderer spriteRender;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        switch(niveau)
        {
            case IANiveau.STUPIDE:
                IAstupideUpdate();
                break;

            case IANiveau.PETE:
                IApeteUpdate();
                break;
        }
    }

    public void Init()
    {
        bc2D = this.gameObject.GetComponent<BoxCollider2D>();
        spriteRender = this.gameObject.GetComponent<SpriteRenderer>();
        player = this.gameObject.GetComponent<Player>();
        bc2D.size = spriteRender.bounds.extents * 2;
        balle = GameObject.Find("Baballe");
        ballePosition = balle.GetComponent<Transform>().position;
        ballePreviousPosition = ballePosition;
        offset = (this.transform.localScale.y * this.GetComponent<BoxCollider2D>().size.y / 2) - 10f;

        player.processLimitePos();
    }

    private void IApeteUpdate()
    {
        // on récup la position actuelle de la balle
        ballePosition = balle.GetComponent<Transform>().position;

        // on recupère la direction de la balle
        balleDirection = ballePosition - ballePreviousPosition;

        // si elle va vers le bas, on touche avec le bas de la hitbox, sinon avec le haut
        if (balleDirection.y < 0)
            ballePosition.y -= offset;
        else ballePosition.y += offset;

        if (ballePosition.y > transform.position.y && transform.position.y < player.limitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (ballePosition.y < transform.position.y && transform.position.y > -player.limitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }

    private void IAstupideUpdate()
    {
        ballePosition = balle.GetComponent<Transform>().position;

        if (ballePosition.y > transform.position.y && transform.position.y < player.limitePos)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (ballePosition.y < transform.position.y && transform.position.y > -player.limitePos)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
}
