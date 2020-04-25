using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public abstract class PowerUp : MonoBehaviour
{
    //public abstract float probability { get; }
    public float lifetimeSec = 5f;
    public float durationSec = 5f;
    public Vector2 Size = new Vector2(0.5f, 0.5f);
    public bool triggered = false;
    private Coroutine LifeCycleCoroutine;
    private Coroutine EffectDurationCoroutine;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        LifeCycleCoroutine = StartCoroutine(LifeCycle());

        // setup size
        Vector3 spriteSize = this.gameObject.GetComponent<SpriteRenderer>().bounds.extents * 2;
        Size = Size * TerrainMaker.unitPix;
        this.gameObject.transform.localScale = Size / spriteSize;

    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public abstract void ApplyEffect();
    public abstract void RevertEffect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PowerUp "+ this.gameObject.name+" triggered by "+ collision.gameObject.tag);
        if(collision.gameObject.tag == "Ball")
        {
            // switch coroutine
            StopCoroutine(LifeCycleCoroutine);
            EffectDurationCoroutine = StartCoroutine(EffectDuration());

            // apply effect
            ApplyEffect();

            // remove physics
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(lifetimeSec);
        Destroy(this.gameObject);
    }

    private IEnumerator EffectDuration()
    {
        yield return new WaitForSeconds(durationSec);
        RevertEffect();
        Destroy(this.gameObject);
    }


}