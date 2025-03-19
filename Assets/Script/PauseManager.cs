using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("UI Panels")]
    public GameObject pausePanel;   // Panel Pause
    public GameObject settingPanel; // Panel Setting
    public GameObject exitPanel;    // Panel Konfirmasi Exit
    public GameObject restartPanel; // Panel Konfirmasi Restart

    private bool isPaused = false;   // Status game pause
    private bool isInSetting = false; // Apakah di panel setting
    private bool isExitPanelOpen = false; // Apakah panel exit terbuka
    private bool isRestartPanelOpen = false; // Apakah panel restart terbuka

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);
        exitPanel.SetActive(false);
        restartPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isRestartPanelOpen)
            {
                CloseRestartPanel(); // Jika Restart Panel terbuka, tutup dulu
            }
            else if (isExitPanelOpen)
            {
                CloseExitPanel(); // Jika Exit Panel terbuka, tutup dulu
            }
            else if (isInSetting)
            {
                CloseSetting(); // Jika di Setting, kembali ke Pause Panel
            }
            else
            {
                TogglePause(); // Jika di Pause, Resume, jika tidak, Pause
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pause game
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1; // Resume game
            pausePanel.SetActive(false);
        }
    }

    public void OpenSetting()
    {
        isInSetting = true;
        settingPanel.SetActive(true);
        pausePanel.SetActive(false); // Sembunyikan Pause Panel
    }

    public void CloseSetting()
    {
        isInSetting = false;
        settingPanel.SetActive(false);
        pausePanel.SetActive(true); // Kembali ke Pause Panel
    }

    public void OpenExitPanel()
    {
        isExitPanelOpen = true;
        exitPanel.SetActive(true);
        pausePanel.SetActive(false); // Sembunyikan Pause Panel
    }

    public void CloseExitPanel()
    {
        isExitPanelOpen = false;
        exitPanel.SetActive(false);
        pausePanel.SetActive(true); // Kembali ke Pause Panel
    }

    public void ExitToMainMenu()
    {
        Debug.Log("Kembali ke Main Menu...");
        Time.timeScale = 1; // Pastikan game tidak tetap pause
        SceneManager.LoadScene("MainMenu"); // Ganti "MainMenu" dengan nama scene yang sesuai
    }

    public void OpenRestartPanel()
    {
        isRestartPanelOpen = true;
        restartPanel.SetActive(true);
        pausePanel.SetActive(false); // Sembunyikan Pause Panel
    }

    public void CloseRestartPanel()
    {
        isRestartPanelOpen = false;
        restartPanel.SetActive(false);
        pausePanel.SetActive(true); // Kembali ke Pause Panel
    }

    public void RestartGame()
    {
        Debug.Log("🔄 Restarting game...");
        Time.timeScale = 1; // Pastikan game tidak tetap pause
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart scene saat ini
    }
}