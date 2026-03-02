using UnityEngine;
using UnityEngine.AI;

public class ZombieWander : MonoBehaviour
{
    public float range = 5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveRandom", 0, 3f);
    }

    void MoveRandom()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}