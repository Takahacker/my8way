using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("Limites da Arena")]
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -3.5f;
    public float maxY = 3.5f;

    [Header("Segundo inimigo")]
    public int scoreToSpawnSecond = 10;
    public float minDistanceFromPlayer = 2f;

    private bool secondSpawned = false;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        SpawnAt(new Vector2(minX, minY));
    }

    void Update()
    {
        if (!secondSpawned && GameController.Score >= scoreToSpawnSecond)
        {
            secondSpawned = true;
            SpawnAwayFromPlayer();
        }
    }

    void SpawnAt(Vector2 pos)
    {
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }

    void SpawnAwayFromPlayer()
    {
        Vector2 pos;
        int attempts = 0;

        do
        {
            pos = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
            );
            attempts++;
        }
        while (player != null
            && Vector2.Distance(pos, player.position) < minDistanceFromPlayer
            && attempts < 30);

        SpawnAt(pos);
    }
}
