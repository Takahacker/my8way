using UnityEngine;

public static class GameController
{
    private static int collectableCount;
    private static bool initialized;
    private static bool playerDead;
    private static int score;

    public static bool gameOver => playerDead;

    public static bool PlayerDead => playerDead;

    public static void KillPlayer()
    {
        playerDead = true;
    }

    public static int Score => score;

    public static void AddScore()
    {
        score++;
    }
    public static void Init()
    {
        collectableCount = 4;
        initialized = true;
    }

    public static void Init(int initialCollectableCount)
    {
        collectableCount = Mathf.Max(0, initialCollectableCount);
        initialized = true;
    }

    public static void InitFromScene()
    {
        Init(GameObject.FindGameObjectsWithTag("Coletavel").Length);
    }

    public static void ResetState()
    {
        collectableCount = 0;
        initialized = false;
        playerDead = false;
        score = 0;
    }

    public static void Collect()
    {
        if (!initialized)
        {
            InitFromScene();
        }

        collectableCount = Mathf.Max(0, collectableCount - 1);
    }

    public static bool IsInitialized()
    {
        return initialized;
    }

}
