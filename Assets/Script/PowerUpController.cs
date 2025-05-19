using UnityEngine;

public class PointController : MonoBehaviour
{
    public BoxCollider2D PointSpawnerBoundary;

    private void Start()
    {
        RandomSpawnPoints();
    }

    public void RandomSpawnPoints()
    {
        Bounds bounds = this.PointSpawnerBoundary.bounds;
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
        this.transform.position = new Vector2(x, y); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<SnakeController>() != null)
        {
            RandomSpawnPoints();
        }
    }
}
