using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;

    public GameObject[] enemies;
    public GameObject powerup;

    private float zEnemySpawn = 15.0f;
    private float xSpawnRange = 16.0f;
    private float ySpawn = 0.75f;

    public TextMeshProUGUI gameOverText;
    public bool isGameActive; //bool is short for boolean
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI livesText;
    private int lives;
    public GameObject pauseScreen;
    private bool paused;
    private float spawnRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        //check if the user has pressed the p key
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
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
/*
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().UpdateScore(-5);
        }
    }
    */

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        //tells us when to end the game if we're out of lives
        if (lives <= 0)
        {
            //this is a method call, we're "calling" GameOver()
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        Debug.Log("Start Game");
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;
        UpdateScore(0);
        UpdateLives(3);
        StartCoroutine(SpawnTarget());
        
        
    }

    void ChangePaused()
    {
        if (!paused) // if not paused, pause game
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else // else if paused, then unpause it
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

        IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, enemies.Length);
            Instantiate(enemies[index]);
            
        }
    }
}
