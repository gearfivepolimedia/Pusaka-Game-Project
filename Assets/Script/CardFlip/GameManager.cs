using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI[] scoreText;
    public TextMeshProUGUI scorePopupText;
    public TextMeshProUGUI timerText;

    [Header("Panels")]
    public GameObject gameOverPanel;

    [Header("Timer Settings")]
    public float gameTime = 60f;
    private bool isGameActive = true;
    private int score = 0;
    private Card firstCard, secondCard;
    private int totalCards;
    private int matchedCards = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        scorePopupText.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);
        totalCards = FindObjectsOfType<Card>().Length;
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (isGameActive && gameTime > 0)
        {
            gameTime -= Time.deltaTime;
            gameTime = Mathf.Max(gameTime, 0); // Pastikan waktu tidak negatif
            UpdateTimerUI();

            if (gameTime <= 0)
            {
                EndGame();
            }
        }
    }

    private IEnumerator StartTimer()
    {
        while (gameTime > 0)
        {
            yield return null;
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
        if (!isGameActive) return;

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
        ShowScorePopup("+100");
        Destroy(firstCard.gameObject);
        Destroy(secondCard.gameObject);
        matchedCards++;
        CheckAllCardsMatched();
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

    private void CheckAllCardsMatched()
    {
    if (matchedCards >= totalCards / 2) // ✅ Perbaikan dari allCardsMatched
    {
        QuizManager.Instance.StartQuiz(); // ✅ Perbaikan pemanggilan
    }
    }

    public void ShowScorePopup(string text)
    {
    scorePopupText.text = text; // ✅ Gunakan teks dari parameter
    scorePopupText.gameObject.SetActive(true);
     Debug.Log("Pop-up score muncul dengan teks: " + text);
    StartCoroutine(HideScorePopup());
    }

    public void AddScore(int amount)
    {
    score += amount;
    Debug.Log("Skor sekarang: " + score);
    UpdateScoreText();
    }


    private IEnumerator HideScorePopup()
    {
         yield return new WaitForSeconds(1.5f);
        scorePopupText.gameObject.SetActive(false);
    }

    public int GetScore()
    {
    return score;
    }

    public void UpdateScoreText()
    {
    foreach (TextMeshProUGUI text in scoreText)
    {
        if (text != null)
        {
            text.text = score.ToString();
            Debug.Log("UI Score Text di-update: " + score); // 🔍 Debugging
        }
        else
        {
            Debug.LogWarning("Ada UI Score Text yang belum diassign di Inspector!"); // 🚨 Debugging untuk cek di Unity
        }
    }
    }

    public float GetRemainingTime()
    {
    return gameTime; // Pastikan ada variabel timer di GameManager
    }

    private void EndGame()
    {
        isGameActive = false;
        if (score > 0)
        {
            WinManager.Instance.ShowWinPanel();
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }
}
