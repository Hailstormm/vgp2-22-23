using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public GameObject[] enemies;
    public GameObject powerup;

    private float zEnemySpawn = 15.0f;
    private float xSpawnRange = 16.0f;
    private float ySpawn = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
    
    void SpawnRandomEnemy()
    {
        float randomX = Random.Range(-xSpawnRange, xSpawnRange);
        int randomIndex = Random.Range(0, enemies.Length);

        Vector3 spawnPos = new Vector3(randomX, ySpawn, zEnemySpawn);

        Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
        UpdateScore(-5);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().UpdateScore(-5);
        }
    }
}
