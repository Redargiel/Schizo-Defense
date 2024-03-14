using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int maxWave = 10;
    private int currentWave = 0;
    public float waveSpawnInterval = 45f;
    public Spawner spawner;

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        if (!spawner.isSpawning)
        {
            waveSpawnInterval -= Time.deltaTime;
            if (waveSpawnInterval <= 0)
            {
                if (currentWave >= maxWave)
                {
                    // Dosáhli jsme maximálního poètu wavek
                    spawner.StopSpawning();
                }
                else
                {
                    StartNextWave();
                }
            }
        }
    }

    void StartNextWave()
    {
        currentWave++;
        spawner.StartNextWave();
        waveSpawnInterval = 45f; // Resetujeme interval pro další wave
    }
}




