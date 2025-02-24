using UnityEngine;
using UnityEngine.AI;

public class CollectibleController : MonoBehaviour
{
    public float moveRadius = 5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomLocation();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToRandomLocation();
        }
    }

    void MoveToRandomLocation()
    {
        Vector3 randomPoint = GetRandomPoint(transform.position, moveRadius);
        agent.SetDestination(randomPoint);
    }

    Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }
}
