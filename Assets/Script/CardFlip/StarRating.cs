using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarRating : MonoBehaviour
{
    public static StarRating Instance;

    [Header("Star UI Elements")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public TextMeshProUGUI hiddenText; // Teks yang hanya muncul jika mendapatkan 3 bintang

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Pastikan semua bintang tersembunyi di awal
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        hiddenText.gameObject.SetActive(false); // Sembunyikan teks saat awal
    }

    public void UpdateStarRating(int score)
    {
        if (score >= 500)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            hiddenText.gameObject.SetActive(true); // Tampilkan teks jika mendapat bintang 3
        }
        else if (score >= 300)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
            hiddenText.gameObject.SetActive(false);
        }
        else if (score >= 100)
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