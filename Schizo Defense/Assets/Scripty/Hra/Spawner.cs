using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 0.5f;
    public List<Transform> wayPoints;
    public int maxCount = 10;
    private int currentWave = 1;

    private int enemiesSpawned = 0;

    public bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        enemiesSpawned = 0;
        int totalEnemies = currentWave * 10;
        while (enemiesSpawned < totalEnemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetDestination(wayPoints);
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnRate);
        }
        isSpawning = false;
    }

    public void StartNextWave()
    {
        currentWave++;
        StartSpawning();
    }

    public void StopSpawning()
    {
        StopCoroutine(SpawnWave());
    }
}