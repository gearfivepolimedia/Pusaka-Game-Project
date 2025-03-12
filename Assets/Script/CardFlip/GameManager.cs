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
        ShowScorePopup();
        Destroy(firstCard.gameObject);
        Destroy(secondCard.gameObject);

        matchedCards++; // Tambahkan jumlah kartu yang cocok
        WinManager.Instance.IncreaseMatchedCards(); // Pastikan ini dipanggil untuk cek kemenangan
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
        scorePopupText.text = "+100";
        scorePopupText.gameObject.SetActive(true);
        StartCoroutine(HideScorePopup());
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

    private void UpdateScoreText()
    {
        foreach (TextMeshProUGUI text in scoreText)
        {
            text.text = score.ToString();
        }
    }

    private void EndGame()
    {
        isGameActive = false;
        if (score > 0)
        {
            WinManager.Instance.ShowWinPanel(score);
        }
        else
        {
            gameOverPanel.SetActive(true);
        }
    }
}
