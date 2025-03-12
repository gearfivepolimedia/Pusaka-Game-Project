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
    public int currentLevel; // Level saat ini
    public int totalCards; // Jumlah total kartu dalam level
    private int matchedCards = 0; // Kartu yang sudah dipasangkan

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
            nextLevelButton.interactable = false; // Tombol tidak aktif sampai level selesai
    }

    public void IncreaseMatchedCards()
    {
        matchedCards++;
        if (matchedCards >= totalCards / 2) // Karena kartu berpasangan
        {
            ShowWinPanel(GameManager.Instance.GetScore());
        }
    }

    public void ShowWinPanel(int score)
    {
        winPanel.SetActive(true);
        finalScoreText.text = score.ToString();

        if (score >= scoreForThreeStars)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            hiddenText.gameObject.SetActive(true);
        }
        else if (score >= scoreForTwoStars)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            hiddenText.gameObject.SetActive(false);
        }
        else if (score >= scoreForOneStar)
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
        PlayerPrefs.SetInt("Level" + nextLevel, 1); // Menyimpan level yang terbuka
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
