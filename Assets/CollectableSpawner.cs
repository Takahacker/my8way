using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    public static CollectableSpawner Instance { get; private set; }

    [Header("Prefab")]
    public GameObject collectablePrefab;

    [Header("Limites da Arena")]
    public float minX = -6f;
    public float maxX = 6f;
    public float minY = -3.5f;
    public float maxY = 3.5f;

    void Awake()
    {
        Instance = this;
        SpawnNext();
    }

    public void SpawnNext()
    {
        Vector2 pos = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
        Instantiate(collectablePrefab, pos, Quaternion.identity);
    }
}
