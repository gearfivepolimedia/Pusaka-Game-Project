using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("Win Panel UI")]
    public GameObject winPanel; // Panel kemenangan
    public TextMeshProUGUI finalScoreText; // Hanya angka, tanpa "Final Score"

    [Header("Star Rating UI")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TextMeshProUGUI hiddenText; // Teks tersembunyi hanya untuk bintang 3

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
        finalScoreText.text = score.ToString(); // Menampilkan hanya angka skor

        // Menentukan jumlah bintang berdasarkan skor
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