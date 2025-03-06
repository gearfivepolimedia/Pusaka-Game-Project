using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance;

    public Image[] healthIcons; // Array untuk ikon nyawa
    public GameObject gameOverPanel; // Panel Game Over

    private int health;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        health = healthIcons.Length; // Health sesuai jumlah ikon
        gameOverPanel.SetActive(false); // Sembunyikan panel game over saat awal
    }

    public void LoseHealth()
    {
        if (health > 0)
        {
            health--;
            healthIcons[health].enabled = false; // Nonaktifkan ikon health terakhir

            if (health <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
