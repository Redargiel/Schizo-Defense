using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private List<Transform> wayPoints;
    private int currentWayPointIndex = 0;
    private NavMeshAgent agent;
    private LevelManager levelManager; // Reference na level manager
    public Slider healthBarPrefab;
    private Slider healthBar;
    public int maxHealth = 100;
    public LayerMask towerLayer;
    private float agentStoppingDistance = 0.3f;
    private bool wayPointSet = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>(); // Najde se LevelManager ve scénì

        // Instantiate HP bar
        healthBar = Instantiate(healthBarPrefab, Vector3.zero, Quaternion.identity);
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform); // Nastavit rodièe na Canvas
    }

    void Update()
    {
        if (!wayPointSet)
            return;

        // Aktualizace pozice HP baru
        if (healthBar)
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);

        // Pohyb nepøítele
        if (!agent.pathPending && agent.remainingDistance <= agentStoppingDistance)
        {
            if (currentWayPointIndex == wayPoints.Count)
            {
                Destroy(gameObject, 0.1f);
                Destroy(healthBar.gameObject, 0.1f);
            }
            else
            {
                currentWayPointIndex++;
                agent.SetDestination(wayPoints[currentWayPointIndex].position);
            }
        }
    }

    // Nastavení cílových bodù pohybu
    public void SetDestination(List<Transform> wayPoints)
    {
        this.wayPoints = wayPoints;
        currentWayPointIndex = 0;
        wayPointSet = true;
    }

    // Poškození nepøítele
    public void Hit(int damage)
    {
        if (healthBar)
        {
            healthBar.value -= damage;
            if (healthBar.value <= 0)
            {
                // Zjistí, zda-li byla znièena vìž
                float range = 15f;
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, towerLayer);
                foreach (var hitCollider in hitColliders)
                {
                    MinigunVez tower = hitCollider.GetComponent<MinigunVez>();
                    if (tower != null)
                        tower.EnemyDestroyed(gameObject);
                }

                // Znièení HP baru a nepøítele
                Destroy(healthBar.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

