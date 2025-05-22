using UnityEngine;

public enum FoodType 
{ 
    MassGainer, 
    MassBurner 
}

public enum PowerUpType 
{ 
    Shield, 
    ScoreBoost, 
    SpeedUp 
}

public class PointController : MonoBehaviour
{
    public BoxCollider2D PointSpawnerBoundary;
    public GameObject[] FoodPrefabs;
    public GameObject[] PowerUpPrefabs;
    public float FoodLifetime = 6f;
    public float SpawnIntervalMin = 2f;
    public float SpawnIntervalMax = 4f;

    private void Start()
    {
        Invoke(nameof(SpawnRandomItem), Random.Range(SpawnIntervalMin, SpawnIntervalMax));
    }

    private void SpawnRandomItem()
    {
        bool spawnFood = Random.value > 0.5f;
        GameObject prefabToSpawn;

        if (spawnFood)
        {
            prefabToSpawn = FoodPrefabs[Random.Range(0, FoodPrefabs.Length)];
        }
        else
        {
            prefabToSpawn = PowerUpPrefabs[Random.Range(0, PowerUpPrefabs.Length)];
        }

        Bounds bounds = PointSpawnerBoundary.bounds;
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
        Vector2 spawnPos = new Vector2(x, y);

        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
        Destroy(obj, FoodLifetime);

        Invoke(nameof(SpawnRandomItem), Random.Range(SpawnIntervalMin, SpawnIntervalMax));
    }
}
