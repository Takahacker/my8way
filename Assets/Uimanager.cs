using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Uimanager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;
    public TimerManager timerManager;
    public Button restartButton;
    public Button menuButton;
    public AudioClip deathSound;
    public AudioClip gameplayMusic;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    private AudioSource musicSource;
    private bool hasFinished;

    void Start()
    {
        Time.timeScale = 1f;
        endGamePanel.SetActive(false);
        hasFinished = false;

        if (timerManager == null)
            timerManager = FindFirstObjectByType<TimerManager>();

        if (timerManager != null)
            timerManager.ResetTimer();

        if (restartButton != null)
            restartButton.onClick.AddListener(Restart);

        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMenu);

        if (gameplayMusic != null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = gameplayMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.ignoreListenerPause = true;
            musicSource.Play();
        }
    }

    void Update()
    {
        if (hasFinished)
            return;

        bool died = GameController.PlayerDead;
        bool timeUp = timerManager != null && timerManager.IsTimeUp();

        if (scoreText != null)
            scoreText.text = "Pontos: " + GameController.Score;

        if (died || timeUp)
        {
            hasFinished = true;

            if (scoreText != null)
                scoreText.gameObject.SetActive(false);

            if (timerManager != null)
            {
                if (timerManager.timerText != null)
                    timerManager.timerText.gameObject.SetActive(false);
                timerManager.FinishTimer();
            }

            if (resultText != null)
                resultText.text = died ? "Voce morreu!" : "Tempo esgotado!";

            if (finalScoreText != null)
                finalScoreText.text = "Pontuacao: " + GameController.Score;

            if (musicSource != null)
                musicSource.Stop();

            if (died && deathSound != null)
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);

            endGamePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void Restart()
    {
        Time.timeScale = 1f;
        GameController.ResetState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToMenu()
    {
        Time.timeScale = 1f;
        GameController.ResetState();
        SceneManager.LoadScene(0);
    }
}
