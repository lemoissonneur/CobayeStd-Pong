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
    public Vector2 SizePix = new Vector2(0.5f, 0.5f);

    private bool triggered = false;
    public bool Triggered
    {
        get => triggered;
    }
    private Coroutine LifeCycleCoroutine;
    private Coroutine EffectDurationCoroutine;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        LifeCycleCoroutine = StartCoroutine(LifeCycle());
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public abstract void ApplyEffect();
    public abstract void RevertEffect();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            triggered = true;

            // switch coroutine
            StopCoroutine(LifeCycleCoroutine);
            EffectDurationCoroutine = StartCoroutine(EffectDuration());

            // apply effect
            ApplyEffect();

            // remove physics
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else Debug.Log("PowerUp '" + this.gameObject.name + "' triggered by " + collision.gameObject.name);
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

    public void SetSizePix(Vector2 newSizePix)
    {
        Vector3 spriteSize = this.gameObject.GetComponent<SpriteRenderer>().bounds.extents * 2;
        this.gameObject.transform.localScale = newSizePix / spriteSize;
    }

}