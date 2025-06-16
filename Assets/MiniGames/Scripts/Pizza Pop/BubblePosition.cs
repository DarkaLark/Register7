using UnityEngine;

public class BubblePosition : MonoBehaviour
{
    [SerializeField] GameObject _bubblePrefab;
    [SerializeField] Transform _spawnCenter;
    [SerializeField] float _radius = 0.5f;

    public void SpawnBubble()
    {
        Vector3 spawnPosition = RandomPointOnXZPlane();
        Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawning");
    }

    private Vector2 RandomPointOnPizza()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * _radius;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
    }

    private Vector3 RandomPointOnXZPlane()
    {
        Vector2 spawnPosition = RandomPointOnPizza();
        Vector3 center = _spawnCenter.position;
        return new Vector3(center.x + spawnPosition.x, center.y, center.z + spawnPosition.y);
    }
}