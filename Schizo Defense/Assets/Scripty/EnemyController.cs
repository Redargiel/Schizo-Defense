using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private List<Transform> wayPoints;

    private int currentWayPointIndex = 0;

    private float agentStoppingDistance = 0.3f;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance <= agentStoppingDistance)
        {
            if (currentWayPointIndex == wayPoints.Count - 1)
            {
                Destroy(this.gameObject, 0.1f);
            }
            else
            {
                currentWayPointIndex++;
                agent.SetDestination(wayPoints[currentWayPointIndex].position);
            }
        }
    }
}
