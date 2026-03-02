using UnityEngine;
using UnityEngine.AI;

public class ZombieChase : MonoBehaviour
{
    public float stopDistance = 1.6f;   // 追到多近就停
    public Transform target;            // 要追谁（玩家）

    private NavMeshAgent agent;
    private ZombieWander wander;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        wander = GetComponent<ZombieWander>();
    }

    void Update()
    {
        if (target == null || agent == null) return;

        // 被打后：关掉随机走
        if (wander != null && wander.enabled)
            wander.enabled = false;

        agent.stoppingDistance = stopDistance;
        agent.SetDestination(target.position);
    }

    // 让外部调用：开始追某个人
    public void StartChase(Transform t)
    {
        target = t;
    }
}