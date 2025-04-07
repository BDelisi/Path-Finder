using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public bool playerTurn = true;
    public float turnCD;
    public string nextLv = "GameOver";
    public GameObject warning;
    public List<GameObject> warnings;
    public List<Enemy> enemies;

    private GameObject player;
    private Player playerScript;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        enemies.Clear();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerTurn)
        {
            turnCD -= Time.deltaTime;
            if(turnCD <= 0 )
            {
                playerTurn = true;
            }
        }
    }

    public void EnemyTurn()
    {
        playerTurn = false;
        turnCD = .25f;
        foreach (GameObject obj in warnings)
        {
            obj.GetComponent<Warning>().Attack();
        }
        warnings.Clear();
        foreach (Enemy theEnemy in enemies)
        {
            theEnemy.Turn();
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLv()
    {
        SceneManager.LoadScene(nextLv);
    }

    public void CreateWarning(Vector2 pos)
    {
        warnings.Add(Instantiate(warning, pos, Quaternion.identity));
    }
}
