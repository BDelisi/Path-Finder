using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float lifespan = .5f;
    public float distance;
    public Vector2 direction;
    public bool playingAnimation = false;

    private float remainingLifeSpan;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        float angle = Mathf.Atan2(direction.x, direction.y * -1f) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
    }

    // Update is called once per frame
    void Update()
    {
        if (playingAnimation)
        {
            transform.position += (Vector3)direction * ((distance / lifespan) * Time.deltaTime) ;
            remainingLifeSpan -= Time.deltaTime;
            if (remainingLifeSpan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetStats(float lifespan, Vector2 direction, float distance)
    {
        this.lifespan = lifespan;
        this.distance = distance;
        this.direction = direction;
    }

    public void StartAnimation()
    {
        remainingLifeSpan = lifespan;
        playingAnimation = true;
        spriteRenderer.enabled = true;
        GetComponent<AudioSource>().Play();
    }
}