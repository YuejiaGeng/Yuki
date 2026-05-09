using UnityEngine;
using UnityEngine.AI;

public class ZombieWander : MonoBehaviour
{
    public float range = 5f;
    public float detectRange = 8f;

    private NavMeshAgent agent;
    private Transform player;
    private float moveTimer;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        moveTimer = 3f;

        if (animator != null)
        {
            animator.SetBool("Grounded", true);
            animator.SetBool("Static_b", false);
        }
    }

    void Update()
    {
        if (agent == null) return;

        // 更新走路动画
        UpdateAnimation();

        // 只有找到玩家时才计算追击
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectRange)
            {
                agent.SetDestination(player.position);
                return;
            }
        }

        // 没追玩家时，继续随机移动
        moveTimer += Time.deltaTime;

        if (moveTimer >= 3f)
        {
            MoveRandom();
            moveTimer = 0f;
        }
    }

    void MoveRandom()
    {
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        float speed = agent.velocity.magnitude;

        animator.SetFloat("Speed_f", speed);
        animator.SetBool("Grounded", true);

        // 移动时取消静止状态，停下时恢复静止状态
        if (speed > 0.1f)
        {
            animator.SetBool("Static_b", false);
        }
        else
        {
            animator.SetBool("Static_b", true);
        }
    }
}