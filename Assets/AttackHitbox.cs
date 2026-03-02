using UnityEngine;
using System.Collections.Generic;

public class AttackHitbox : MonoBehaviour
{
    [Header("Hit Settings")]
    public float radius = 1.6f;
    public int damage = 1;
    public LayerMask targetLayers; // 只勾 Zombie 层

    private readonly HashSet<ZombieHealth> _hitThisSwing = new HashSet<ZombieHealth>();

    public void DoHitOnce()
    {
        _hitThisSwing.Clear();

        Vector3 center = transform.position;
        Collider[] hits = Physics.OverlapSphere(center, radius, targetLayers, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < hits.Length; i++)
        {
            ZombieHealth zh = hits[i].GetComponentInParent<ZombieHealth>();
            if (zh == null) continue;

            if (_hitThisSwing.Contains(zh)) continue;
            _hitThisSwing.Add(zh);

            zh.TakeDamage(damage);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawSphere(transform.position, radius);
    }
#endif
}