using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int maxWave = 5;

    private int currentWave = 0;

    private bool isSpawning = false;

    private int enemiesRemaining = 0;

    private float timer = 0f;

    public float waveSpawnInterval = 45f;

    public Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            //Dokud se spawnuj� tak timer bude 0
            timer = 0;
        }
        else
        {
            //Pokud se nespawnuj� zastav� se countdown
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (currentWave >= maxWave)
                {
                    //Zda jsme dos�hli maxim�ln� po�et wavek, tak se zastav� spawning
                    StopSpawning();
                }
                else
                {
                    StartNextWave();
                }
            }
        }
    }

    public void EnemyDestroyed()
    {
        enemiesRemaining--;
        if (enemiesRemaining == 0)
        {
            isSpawning = false;
            // V�echny enem�ci byli zni�eni, resetuje se na waveSpawnInterval
            timer = waveSpawnInterval;
        }
    }
    void StartNextWave()
    {
        currentWave++;
        spawner.StartNextWave();
        enemiesRemaining = spawner.maxCount;
        isSpawning = true;
    }

    void StopSpawning()
    {
        spawner.StopSpawning();
        isSpawning = false;
    }    
}
