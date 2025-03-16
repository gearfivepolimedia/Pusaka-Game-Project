using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("Win Panel UI")]
    public GameObject winPanel;
    public TextMeshProUGUI finalScoreText;
    public Button nextLevelButton;
    public QuizManager quizManager;

    [Header("Star Rating UI")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TextMeshProUGUI hiddenText;

    [Header("Star Score Thresholds")]
    public int scoreForOneStar = 100;
    public int scoreForTwoStars = 300;
    public int scoreForThreeStars = 500;

    [Header("Level Settings")]
    public int currentLevel;
    public int totalCards;
    private int matchedCards = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        winPanel.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        hiddenText.gameObject.SetActive(false);

        if (nextLevelButton != null)
            nextLevelButton.interactable = false;
    }

    public void StartQuiz()
    {
        quizManager.StartQuiz();
    }

    public void IncreaseMatchedCards()
    {
        matchedCards++;
        if (matchedCards >= totalCards / 2)
        {
            StartQuiz();
        }
    }

    public void ShowWinPanel()
    {
        int quizScore = (quizManager != null) ? quizManager.GetQuizScore() : 0;
        int totalScore = GameManager.Instance.GetScore() + quizScore;
        
        winPanel.SetActive(true);
        finalScoreText.text = totalScore.ToString();

        // Hitung bintang berdasarkan total skor
        if (totalScore >= scoreForThreeStars)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            hiddenText.gameObject.SetActive(true);
        }
        else if (totalScore >= scoreForTwoStars)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            hiddenText.gameObject.SetActive(false);
        }
        else if (totalScore >= scoreForOneStar)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
            hiddenText.gameObject.SetActive(false);
        }

        UnlockNextLevel();
    }

    private void UnlockNextLevel()
    {
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetInt("Level" + nextLevel, 1);
        PlayerPrefs.Save();

        if (nextLevelButton != null)
            nextLevelButton.interactable = true;
    }

    public void LoadNextLevel()
    {
        int nextLevel = currentLevel + 1;
        if (PlayerPrefs.GetInt("Level" + nextLevel, 0) == 1)
        {
            SceneManager.LoadScene("Level" + nextLevel);
        }
    }
}
