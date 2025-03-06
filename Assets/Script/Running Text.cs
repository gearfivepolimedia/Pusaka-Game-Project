using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RunningText : MonoBehaviour
{
    [Header("Text Setting")]
    [SerializeField] [TextArea] private string[] itemInfo;
    [SerializeField] private float textSpeed = 0.01f;
    [SerializeField] private float delayBetweenTexts = 2f;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI itemInfoText;
    [SerializeField] private Button autoButton;
    
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName; // Nama scene yang dipilih di Inspector

    private int currentDisplayingText = 0;
    private bool isTextRunning = false;
    private bool isAutoMode = false;

    private void Start()
    {
        autoButton.onClick.AddListener(ToggleAutoMode);
        StartCoroutine(LoopText());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isTextRunning)
            {
                StopAllCoroutines();
                itemInfoText.text = itemInfo[currentDisplayingText];
                isTextRunning = false;
            }
            else if (currentDisplayingText < itemInfo.Length - 1 && !isAutoMode)
            {
                currentDisplayingText++;
                StartCoroutine(LoopText());
            }
            else if (currentDisplayingText >= itemInfo.Length - 1)
            {
                LoadNextScene();
            }
        }
    }

    private void ToggleAutoMode()
    {
        isAutoMode = !isAutoMode;
        if (isAutoMode && !isTextRunning)
        {
            if (itemInfoText.text == itemInfo[currentDisplayingText])
            {
                StartCoroutine(AutoLoopText());
            }
            else
            {
                StopAllCoroutines();
                itemInfoText.text = itemInfo[currentDisplayingText];
                isTextRunning = false;
                StartCoroutine(AutoLoopText());
            }
        }
    }

    private IEnumerator AutoLoopText()
    {
        while (isAutoMode && currentDisplayingText < itemInfo.Length - 1)
        {
            currentDisplayingText++;
            yield return StartCoroutine(LoopText());
        }
    }

    private IEnumerator LoopText()
    {
        isTextRunning = true;
        yield return StartCoroutine(AnimateText());
        isTextRunning = false;
        yield return new WaitForSeconds(delayBetweenTexts);
    }

    private IEnumerator AnimateText()
    {
        itemInfoText.text = "";
        for (int i = 0; i < itemInfo[currentDisplayingText].Length + 1; i++)
        {
            itemInfoText.text = itemInfo[currentDisplayingText].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void LoadNextScene()
    {
        // Load the scene set in the inspector
        SceneManager.LoadScene(nextSceneName);
    }
}
