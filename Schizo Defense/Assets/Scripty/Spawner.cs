using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 0.5f;

    public List<Transform> wayPoints;

    public int maxCount = 10;

    private int count = 0;

    void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().SetDestination(wayPoints);

        count++;

        if (count >= maxCount)
        {
            CancelInvoke();
        }
    }

    public void StartNextWave()
    {
        count = 0;
        InvokeRepeating("Spawn", 1, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke();
    }
}
