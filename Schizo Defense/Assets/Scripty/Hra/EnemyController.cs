using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public LevelManager levelManager; //Reference na level manager

    private List<Transform> wayPoints;

    public LayerMask towerLayer;

    private int currentWayPointIndex = 0;

    private float agentStoppingDistance = 0.3f;

    private bool wayPointSet = false;

    public Slider healthBarPrefab;

    Slider healthBar;

    public int maxHealth = 100;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>(); // Najde se LevelManager ve scénì

        healthBar = Instantiate(healthBarPrefab, this.transform.position, Quaternion.identity);
        healthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wayPointSet)
        {
            return;
        }
        if (healthBar)
        {
            healthBar.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 2f);
        }
        if (!agent.pathPending && agent.remainingDistance <= agentStoppingDistance)
        {
            if (currentWayPointIndex == wayPoints.Count)
            {
                levelManager.EnemyDestroyed();
                Destroy(this.gameObject, 0.1f);
                Destroy(healthBar.gameObject, 0.1f);
            }
            else
            {
                currentWayPointIndex++;
                agent.SetDestination(wayPoints[currentWayPointIndex].position);
            }
        }
    }

    public void SetDestination(List<Transform> wayPoints)
    {
        this.wayPoints = wayPoints;
        wayPointSet = true;
    }

    public void Hit(int damage)
    {
        if (healthBar)
        {
            healthBar.value -= damage;
            if (healthBar.value <= 0)
            {
                float range = 15f;

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, towerLayer);

                foreach (var hitCollider in hitColliders)
                {
                    MinigunVez tower = hitCollider.GetComponent<MinigunVez>();
                    if (tower != null)
                    {
                        tower.EnemyDestroyed(gameObject);
                    }
                }
                Destroy(healthBar.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}