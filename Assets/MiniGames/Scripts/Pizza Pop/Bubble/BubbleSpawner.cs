using UnityEngine;
using System.Collections;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Spawning")]
    [Space(5)]
    [SerializeField] GameObject _bubblePrefab;
    [SerializeField] Transform _spawnCenter;
    [Space(5)]
    [SerializeField] private float _radius = 1f;

    [Header("Difficulty")]
    [Space(5)]
    private int _numberOfBubbles;
    private int _minBubbles = 8;
    private int _maxBubbles = 10;
    private int _currentBubble = 0;

    [Header("Game Events")]
    [Space(5)]
    [SerializeField] private GameEvent _onBubbleSpawn;

    void Start()
    {
        _numberOfBubbles = Random.Range(_minBubbles, _maxBubbles);
        StartCoroutine(StartSpawningBubbles());
    }

    private IEnumerator StartSpawningBubbles()
    {
        while (_numberOfBubbles > _currentBubble)
        {
            SpawnBubble();
            float nextSpawnTimer = Random.Range(1f, 4f);

            yield return new WaitForSeconds(nextSpawnTimer);
        }
    }

#region SpawnBubble func's
        public void SpawnBubble()
        {
            Vector3 spawnPosition = RandomPointOnXZPlane();
            GameObject bubbleObj = Instantiate(_bubblePrefab, spawnPosition, Quaternion.identity);

            Bubble bubble = bubbleObj.GetComponent<Bubble>();
            if (bubble != null)
            {
                bubble.IsLastBubble = (_currentBubble == _numberOfBubbles - 1);
            }

            _currentBubble++;
            _onBubbleSpawn.Raise();
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
#endregion
}