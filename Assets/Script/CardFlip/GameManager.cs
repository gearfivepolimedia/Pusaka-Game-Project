using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI[] scoreText; // Multiple UI untuk score
    public TextMeshProUGUI scorePopupText; // Text popup untuk score
    public TextMeshProUGUI timerText; // UI untuk Timer

    [Header("Panels")]
    public GameObject gameOverPanel; // Panel Game Over

    [Header("Timer Settings")]
    public float gameTime = 60f; // Waktu bisa diatur di Inspector
    private bool isGameActive = true;

    private int score = 0;
    private Card firstCard, secondCard;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        scorePopupText.gameObject.SetActive(false); // Sembunyikan popup saat awal
        gameOverPanel.SetActive(false); // Sembunyikan Game Over panel
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (isGameActive && gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private IEnumerator StartTimer()
    {
        while (gameTime > 0)
        {
            yield return null;
        }

        isGameActive = false;

        // Jika waktu habis dan skor masih 0, tampilkan Game Over Panel
        if (score == 0)
        {
            gameOverPanel.SetActive(true);
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CardSelected(Card card)
    {
        if (!isGameActive) return; // Tidak bisa memilih kartu jika permainan berakhir

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.GetSprite() == secondCard.GetSprite())
        {
            score += 100;
            ShowScorePopup();
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
        }
        else
        {
            firstCard.FlipBack();
            secondCard.FlipBack();
            HealthManager.Instance.LoseHealth();
        }

        firstCard = null;
        secondCard = null;

        UpdateScoreText();
    }

    private void ShowScorePopup()
    {
        scorePopupText.text = "+100"; // Set teks popup
        scorePopupText.gameObject.SetActive(true); // Un-hide popup

        StartCoroutine(HideScorePopup());
    }

    private IEnumerator HideScorePopup()
    {
        yield return new WaitForSeconds(1.5f);
        scorePopupText.gameObject.SetActive(false);
    }

    private void UpdateScoreText()
    {
        foreach (TextMeshProUGUI text in scoreText)
        {
            text.text = score.ToString();
        }
    }
}