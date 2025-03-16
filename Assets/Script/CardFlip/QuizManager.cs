using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance; // Singleton
    private bool isQuizFinished = false;
    private bool answerProcessed = false;
    

    [Header("UI Elements")]
    public GameObject quizPanel; // Panel untuk quiz
    public TextMeshProUGUI questionText;
    public Button[] answerButtons; // Button pilihan jawaban (A, B, C, D)

    [Header("Quiz Data")]
    public List<QuizQuestion> questions = new List<QuizQuestion>(); // List soal di Inspector
    private int currentQuestionIndex = 0;
    private int quizScore = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        quizPanel.SetActive(false);
    }

    public void StartQuiz()
    {
        quizPanel.SetActive(true);
        currentQuestionIndex = 0;
        quizScore = 0;  // 🔴 Reset score setiap kali quiz dimulai
        isQuizFinished = false; // 🔴 Reset flag quiz selesai
        LoadQuestion();
    }

    private void LoadQuestion()
   {
    if (currentQuestionIndex < questions.Count)
    {
        answerProcessed = false; // ✅ Reset flag agar bisa menjawab lagi di soal berikutnya

        QuizQuestion question = questions[currentQuestionIndex];
        questionText.text = question.question;
        Debug.Log($"📖 Menampilkan soal ke-{currentQuestionIndex}: {question.question}");

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].interactable = true;

            int index = i;
            answerButtons[i].onClick.AddListener(() => AnswerSelected(index));

            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.answers[i];

            Debug.Log($"📝 Menambahkan event ke tombol {index} dengan jawaban: {question.answers[i]}");
        }
    }
}

    public void AnswerSelected(int index)
    {
    if (answerProcessed) return; // ✅ Jika sudah diproses, hentikan

    answerProcessed = true; // ✅ Tandai sudah diproses agar tidak dihitung lagi

    if (currentQuestionIndex >= questions.Count)
    {
        Debug.LogWarning("⚠️ Index soal melebihi jumlah soal yang tersedia! Jawaban tidak diproses.");
        return;
    }

    // Nonaktifkan semua tombol agar tidak bisa ditekan dua kali
    foreach (Button btn in answerButtons)
        btn.interactable = false;

    bool isCorrect = index == questions[currentQuestionIndex].correctAnswer;

    if (isCorrect)
    {
        quizScore += 100; // ✅ Pastikan skor hanya bertambah 100 per soal
        Debug.Log("✅ Jawaban benar! Menambah skor.");
        GameManager.Instance.AddScore(100);
        GameManager.Instance.ShowScorePopup("+100");
        GameManager.Instance.UpdateScoreText();
    }
    else
    {
        Debug.Log("❌ Jawaban salah! Mengurangi nyawa.");
        HealthManager.Instance.LoseHealth();
    }

    if (currentQuestionIndex + 1 < questions.Count)
    {
        currentQuestionIndex++;
        StartCoroutine(NextQuestionDelay());
    }
    else
    {
        if (!isQuizFinished)
        {
            FinishQuiz(); // ✅ Langsung panggil tanpa delay
        }
    }
    }

    private IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(1.0f); // Tunggu sebelum lanjut ke soal berikutnya
        LoadQuestion();
    }

    private void FinishQuiz()
    {
        if (isQuizFinished) return; // ✅ Cegah pemanggilan ganda

        isQuizFinished = true; // ✅ Tandai quiz sudah selesai
        quizPanel.SetActive(false);

        // Nonaktifkan semua tombol jawaban agar tidak bisa diklik lagi
        foreach (Button btn in answerButtons)
        {
            btn.interactable = false;
            btn.onClick.RemoveAllListeners();
        }

        Debug.Log("🏆 Quiz selesai! Menampilkan Win Panel...");
        WinManager.Instance.ShowWinPanel(); // Tampilkan Win Panel setelah quiz selesai
    }

    private IEnumerator FinishQuizDelay()
    {
    if (isQuizFinished)
    {
        Debug.Log("⚠️ FinishQuizDelay() sudah berjalan sebelumnya, tidak memproses lagi.");
        yield break;
    }

    isQuizFinished = true; // ✅ Tandai quiz selesai
    Debug.Log("🕒 Semua soal telah dijawab. Menampilkan Win Panel dalam 3 detik...");
    
    yield return new WaitForSeconds(3.0f);
    
    Debug.Log("🏆 Memanggil FinishQuiz()...");
    FinishQuiz();
    }


    public int GetQuizScore()
    {
        return quizScore;
    }

    [System.Serializable]
    public class QuizQuestion
    {
    public string question;
    public string[] answers = new string[4]; // 4 Pilihan: A, B, C, D
    public int correctAnswer; // Index jawaban yang benar (0=A, 1=B, 2=C, 3=D)
    }
}
