using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 4;
    private int currentHealth;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text timerText;
    public TMP_Text highScoreText;
    public TMP_Text playerWinLoseText;
    public GameObject gameOverPanel;
    private int score;
    private float timer;
    private float highScore;
    private List<GameObject> enemies;

    private void Start()
    {
        currentHealth = maxHealth;
        score = 0;
        timer = 0f;
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        gameOverPanel.SetActive(false);
        enemies = new List<GameObject>();
        UpdateUI();
    }

    private void Update()
    {
        if (currentHealth > 0)
        {
            timer += Time.deltaTime;
            UpdateUI();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        CheckHighScore();
        playerWinLoseText.text = "You Lose!";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyTag"))
        {
            collision.gameObject.SetActive(false);
            score++;
            enemies.Add(collision.gameObject);

            if (score >= 4)
            {
                Time.timeScale = 0;
                CheckHighScore();
                playerWinLoseText.text = "You Win!";
                gameOverPanel.SetActive(true);
            }

            UpdateUI();
        }
    }

    public void Reset()
    {
        score = 0;
        currentHealth = maxHealth;
        timer = 0f;
        UpdateUI();
        gameObject.SetActive(true);

        StartCoroutine(RespawnEnemies());

        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }

    private void CheckHighScore()
    {
        if (timer > highScore)
        {
            highScore = timer;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + currentHealth;
        timerText.text = "Time: " + timer.ToString("F2");
        highScoreText.text = "High Score: " + highScore.ToString("F2");
    }

    private IEnumerator RespawnEnemies()
    {
        yield return new WaitForSeconds(2f);
        // access the enemy list and set them active


        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(true);
        }
        timer = 0f;
       enemies.Clear();
    }
}