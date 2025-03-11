using UnityEngine;
using TMPro;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("Win Panel UI")]
    public GameObject winPanel;
    public TextMeshProUGUI finalScoreText;

    [Header("Star Rating UI")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TextMeshProUGUI hiddenText;

    [Header("Star Score Thresholds")]
    public int scoreForOneStar = 100;
    public int scoreForTwoStars = 300;
    public int scoreForThreeStars = 500;

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
        else
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
            hiddenText.gameObject.SetActive(false);
        }
    }
}
