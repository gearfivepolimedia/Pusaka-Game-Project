using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;    
    public Button settingsButton; 
    public Button exitButton;    

    public GameObject settingsPanel; 
    public string playSceneName;     

    void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(PlayGame);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(ToggleSettings);

        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);

        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(playSceneName);
    }

    void ToggleSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); 
    }
}