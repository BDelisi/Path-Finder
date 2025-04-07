using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifespan = .5f;
    public Vector2 endSize = Vector2.one;
    public float growSpeedMultiplier = 1f;
    public bool playingAnimation = false;

    private float remainingLifeSpan;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playingAnimation)
        {
            if (transform.localScale.x < endSize.x)
            {
                transform.localScale += new Vector3((endSize.x / 2) / lifespan * Time.deltaTime * growSpeedMultiplier, (endSize.y / 2) / lifespan * Time.deltaTime * growSpeedMultiplier, 0);
            }
            remainingLifeSpan -= Time.deltaTime;
            if (remainingLifeSpan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetStats(float lifespan, Vector2 endSize, float growSpeedMultiplier)
    {
        this.lifespan = lifespan;
        this.endSize = endSize;
        this.growSpeedMultiplier = growSpeedMultiplier;
    }

    public void StartAnimation()
    {
        remainingLifeSpan = lifespan;
        transform.localScale = endSize / 2;
        playingAnimation = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
