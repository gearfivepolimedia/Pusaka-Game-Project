using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
   public Button ButtonUlang;
   public Button ButtonBackMainMenu;
   public string SceneNameUlang;
   public string SceneNameBackMainMenu;

   void Start()
    {
        if (ButtonUlang != null)
            ButtonUlang.onClick.AddListener(Ulang);
        if (ButtonBackMainMenu != null)
            ButtonBackMainMenu.onClick.AddListener(BackMainMenu);
    }

    void Ulang()
    {
        SceneManager.LoadScene(SceneNameUlang);
    }
     void BackMainMenu()
    {
        SceneManager.LoadScene(SceneNameBackMainMenu);
    }
}
