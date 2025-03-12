using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelLockManager : MonoBehaviour
{
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;
    public float textDuration = 3f; // Bisa diatur di Inspector
    public Button closeButton;
    public Button[] levelButtons;

    private void Start()
    {
        notificationPanel.SetActive(false);
        closeButton.onClick.AddListener(ClosePanel);
        UpdateLevelLocks();
    }

    public void ShowNotification(string message)
    {
        CancelInvoke("ClosePanel"); // Pastikan tidak ada penutupan panel yang tumpang tindih
        notificationText.text = message;
        notificationPanel.SetActive(true);
        Invoke("ClosePanel", textDuration);
    }

    private void ClosePanel()
    {
        notificationPanel.SetActive(false);
    }

    public void TryLoadLevel(int level)
    {
        if (PlayerPrefs.GetInt("Level" + level, 0) == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level);
        }
        else
        {
            Debug.Log("Notifikasi muncul: Level masih terkunci!");
            ShowNotification("Level ini masih terkunci! Selesaikan level sebelumnya terlebih dahulu.");
        }
    }


    private void UpdateLevelLocks()
    {
    for (int i = 0; i < levelButtons.Length; i++)
    {
        int level = i + 1; // Level dimulai dari 1
        bool isUnlocked = PlayerPrefs.GetInt("Level" + level, 0) == 1;

        levelButtons[i].interactable = isUnlocked; // Kunci tombol jika belum terbuka
    }
    }

    public void ResetLevels()
    {
    PlayerPrefs.DeleteAll(); // Hapus semua data progres level
    PlayerPrefs.SetInt("Level1", 1); // Pastikan Level 1 tetap terbuka
    PlayerPrefs.Save(); // Simpan perubahan

    UpdateLevelLocks(); // Perbarui tampilan tombol level

    ShowNotification("Semua level telah direset!");
    }

}
