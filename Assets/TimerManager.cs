using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;

    [Header("Time Settings")]
    public float startingTime = 10f;
    public float bonusPerCollectable = 5f;

    private float remainingTime;
    private float elapsedTime;
    private bool isRunning;
    private bool isFinished;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!isRunning)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            isRunning = false;
        }

        RefreshTimerText();
    }

    public void ResetTimer()
    {
        remainingTime = startingTime;
        elapsedTime = 0f;
        isRunning = true;
        isFinished = false;

        if (finalTimeText != null)
        {
            finalTimeText.text = string.Empty;
        }

        RefreshTimerText();
    }

    public void AddBonusTime()
    {
        if (isFinished)
        {
            return;
        }

        remainingTime += bonusPerCollectable;
        RefreshTimerText();
    }

    public bool IsTimeUp()
    {
        return remainingTime <= 0f;
    }

    public string GetElapsedTimeText()
    {
        return FormatTime(elapsedTime);
    }

    public void FinishTimer()
    {
        if (isFinished)
        {
            return;
        }

        isRunning = false;
        isFinished = true;

        if (finalTimeText != null)
        {
            finalTimeText.text = "Tempo demorado: " + FormatTime(elapsedTime);
        }
    }

    private void RefreshTimerText()
    {
        if (timerText != null)
        {
            timerText.text = "Tempo: " + FormatTime(remainingTime);
        }
    }

    private string FormatTime(float timeValue)
    {
        if (timeValue < 0f)
        {
            timeValue = 0f;
        }

        int minutes = Mathf.FloorToInt(timeValue / 60f);
        int seconds = Mathf.FloorToInt(timeValue % 60f);
        int centiseconds = Mathf.FloorToInt((timeValue * 100f) % 100f);

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
    }
}