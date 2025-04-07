using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public bool hitPlayer = false;
    public GameObject wallChecker;

    private GameManager gameManager;
    private GameObject player;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject test = Instantiate(wallChecker, transform.position, Quaternion.identity);
        if (test.GetComponent<WallChecker>().CheckForWall())
        {
            gameManager.warnings.Remove(gameObject);
            Destroy(gameObject);
        }
        Destroy(test);
    }

    public void Attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= .1f)
        {
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }
}
