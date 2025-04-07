using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public enum EnemyType{wizard, archer};
    public EnemyType enemyType;
    public GameObject particles;
    public Vector2 direction;
    public Vector2 atkSize;
    public int turnCount = 0;
    public int attackCycle = 3;

    private List<GameObject> attackEffect = new List<GameObject>();
    private GameObject player;
    private GameManager gameManager;
    private Player playerScript;
    private Animator animator;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerScript = player.GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Turn()
    {
        turnCount++;
        if (enemyType == EnemyType.wizard)
        {
            if (attackEffect.Count != 0)
            {
                foreach (GameObject effect in attackEffect)
                {
                    effect.GetComponent<Explosion>().StartAnimation();
                }
                attackEffect.Clear();
            }
            if (turnCount % attackCycle == 1)
            {
                Attack(playerScript.lastPos + Vector2.one, atkSize);
                particles.SetActive(true);
            }
            else
            {
                particles.SetActive(false);
            }
        } else if (enemyType == EnemyType.archer)
        {
            if (attackEffect.Count != 0)
            {
                foreach (GameObject effect in attackEffect)
                {
                    effect.GetComponent<Arrow>().StartAnimation();
                }
                attackEffect.Clear();
            }
            
            if (turnCount % attackCycle == 1)
            {
                if (direction.x < 0 || direction.y < 0)
                {
                    Attack((Vector2)transform.position + direction, atkSize);
                } else
                {
                    Attack((Vector2)transform.position + (direction * atkSize), atkSize);
                }
            }
        }

        if (turnCount % attackCycle == 0 || turnCount % attackCycle == 1)
        {
            animator.SetBool("AboutToAttack", true);
        }
        else
        {
            animator.SetBool("AboutToAttack", false);
        }
    }


    public void Attack(Vector2 pos, Vector2 size)
    {
        for (int i = 0; i < size.y; i++)
        {
            for (int j = 0; j < size.x; j++)
            {
                gameManager.CreateWarning(new Vector2(pos.x - j, pos.y - i));
            }
        }
        if (enemyType == EnemyType.wizard)
        {
            GameObject temp = Instantiate(projectile, pos - ((size - Vector2.one) / 2), Quaternion.identity);
            attackEffect.Add(temp);
            temp.GetComponent<Explosion>().SetStats(.25f, size / 3, 1f);
        } else if (enemyType == EnemyType.archer)
        {
            GameObject temp = Instantiate(projectile, (Vector2)transform.position, Quaternion.identity);
            attackEffect.Add(temp);
            temp.GetComponent<Arrow>().SetStats(.25f, direction, size.x + size.y - 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameManager.NextLv();
        }
    }
}
