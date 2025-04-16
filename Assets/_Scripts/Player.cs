using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject wallChecker;
    public Vector2 lastPos;
    public GameObject deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 && gameManager.playerTurn)
        {
            lastPos = transform.position;
            Vector3 targetPos;
            if (Input.GetAxis("Horizontal") > 0)
            {
                targetPos = gameObject.transform.position + new Vector3(1, 0);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                targetPos = gameObject.transform.position + new Vector3(-1, 0);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (CheckMove(targetPos))
            {
                gameManager.EnemyTurn();
            }
        }
        if (Input.GetAxis("Vertical") != 0 && gameManager.playerTurn)
        {
            lastPos = transform.position;
            Vector3 targetPos;
            if (Input.GetAxis("Vertical") > 0)
            {
                targetPos = gameObject.transform.position + new Vector3(0, 1);
            }
            else
            {
                targetPos = gameObject.transform.position + new Vector3(0, -1);
            }
            if (CheckMove(targetPos))
            {
                gameManager.EnemyTurn();
            }
        }
    }

    public bool CheckMove(Vector3 targetPos)
    {
        GameObject test = Instantiate(wallChecker, targetPos, Quaternion.identity);
        if (!test.GetComponent<WallChecker>().CheckWalkable())
        {
            Destroy(test);
            transform.position = targetPos;
            return true;
        } else
        {
            Destroy(test);
            return false;
        }
    }

    public void Death()
    {
        deathParticles.SetActive(true);
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<AudioSource>().Play();
    }
}
