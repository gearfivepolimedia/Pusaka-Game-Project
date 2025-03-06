using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{
    public Button Level1Button;
    public Button BackButton;
    public string Level1SceneName;
    public string BackSceneName;  
   void Start()
    {
        if (Level1Button != null)
            Level1Button.onClick.AddListener(Level1);
        if (Level1Button != null)
            BackButton.onClick.AddListener(Back);
    }

    void Level1()
    {
        SceneManager.LoadScene(Level1SceneName);
    }
     void Back()
    {
        SceneManager.LoadScene(BackSceneName);
    }
}
