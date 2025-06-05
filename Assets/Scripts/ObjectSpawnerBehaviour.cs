using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchmupEnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    float spawnTimer = 0f;
    [SerializeField]
    float spawnFrequency = 5f;
    [SerializeField]
    GameObject minion;
    [SerializeField]
    GameObject spawnPoint;


    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnFrequency)
        {
            Instantiate(minion, spawnPoint.transform.position, Quaternion.identity);
            spawnTimer = 0f;
        }

    }
}