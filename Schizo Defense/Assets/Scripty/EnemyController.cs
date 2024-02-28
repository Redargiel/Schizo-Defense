using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public LevelManager levelManager; //Reference na level manager

    private List<Transform> wayPoints;

    private int currentWayPointIndex = 0;

    private float agentStoppingDistance = 0.3f;

    private bool wayPointSet = false;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>(); // Najde se LevelManager ve scénì
    }

    // Update is called once per frame
    void Update()
    {
        if (!wayPointSet)
        {
            return;
        }
        if (!agent.pathPending && agent.remainingDistance <= agentStoppingDistance)
        {
            if (currentWayPointIndex == wayPoints.Count)
            {
                levelManager.EnemyDestroyed();
                Destroy(this.gameObject, 0.1f);
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
}
