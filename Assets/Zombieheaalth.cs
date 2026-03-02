using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHp = 3;
    public int hp = 3;

    public Transform lastAttacker; // ← 新增

    private HPBarUI hpBar;

    void Awake()
    {
        // 在子物体中自动查找血条（Canvas 上挂着 HPBarUI）
        hpBar = GetComponentInChildren<HPBarUI>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hpBar != null)
            hpBar.TakeHit();

        lastAttacker = GameObject.FindGameObjectWithTag("Player").transform;

        GetComponent<ZombieChase>()?.StartChase(lastAttacker); // ✅ 加这一行

        if (hp <= 0)
            Destroy(gameObject);
    }
}
