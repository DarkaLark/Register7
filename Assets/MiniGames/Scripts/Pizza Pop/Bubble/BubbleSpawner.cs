using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] GameObject _bubblePrefab;

    [Space(5)]
    [SerializeField] Transform _spawnCenter;
    [Space(5)]
    [SerializeField] float _radius = 1f;
    
    void Start()
    {
        StartCoroutine(StartSpawningBubbles());
    }

    private IEnumerator StartSpawningBubbles()
    {
        while (true)
        {
            SpawnBubble();
            float nextSpawnTimer = Random.Range(0f, 2f);

            yield return new WaitForSeconds(nextSpawnTimer);
        };
    }

    public void SpawnBubble()
    {
        Vector3 spawnPosition = RandomPointOnXZPlane();
        Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity);
        Debug.Log("Spawning bubble");
    }

    private Vector3 RandomPointOnXZPlane()
    {
        Vector2 spawnPosition = RandomPointOnPizza();
        Vector3 center = _spawnCenter.position;
        return new Vector3(center.x + spawnPosition.x, center.y, center.z + spawnPosition.y);
    }

    private Vector2 RandomPointOnPizza()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Mathf.Sqrt(Random.Range(0f, 1f)) * _radius;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
    }
}