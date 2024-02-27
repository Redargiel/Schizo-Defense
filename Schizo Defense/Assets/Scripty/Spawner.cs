using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public List<Transform> wayPoints;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1, 0.5f);
    }

    void Spawn()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform);
    }
}
