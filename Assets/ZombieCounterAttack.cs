using UnityEngine;
using System.Collections;

public class ZombieCounterAttack : MonoBehaviour
{
    public float counterStartRange = 3.5f; // 玩家在这个距离内，丧尸才会反击
    public float rushDistance = 2f;        // 丧尸最多向前冲多远
    public float rushSpeed = 5f;           // 冲刺速度
    public int attackDamage = 1;           // 伤害
    public float hitDistance = 2f;         // 冲刺过程中，距离小于这个值才算打到玩家

    public float counterCooldown = 2f;     // 反击冷却时间

    private bool isCountering = false;
    private float lastCounterTime = -999f;

    public void CounterAttack(Transform player)
    {
        if (player == null) return;

        float startDistance = Vector3.Distance(transform.position, player.position);

        // ⭐ 如果玩家太远，丧尸不反击
        if (startDistance > counterStartRange)
        {
            Debug.Log("玩家距离太远，丧尸不反击");
            return;
        }

        // ⭐ 反击冷却
        if (Time.time - lastCounterTime < counterCooldown)
        {
            Debug.Log("丧尸反击冷却中");
            return;
        }

        lastCounterTime = Time.time;

        StopAllCoroutines();
        StartCoroutine(RushToPlayer(player));
    }

    IEnumerator RushToPlayer(Transform player)
    {
        isCountering = true;

        Vector3 startPos = transform.position;
        Vector3 targetDir = (player.position - transform.position).normalized;
        targetDir.y = 0;

        Vector3 targetPos = startPos + targetDir * rushDistance;

        bool hasHitPlayer = false;

        while (Vector3.Distance(transform.position, targetPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                rushSpeed * Time.deltaTime
            );

            if (!hasHitPlayer && player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                if (distanceToPlayer < hitDistance)
                {
                    Debug.Log("丧尸反击打到玩家");

                    PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        hasHitPlayer = true;
                    }
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        isCountering = false;
    }
}