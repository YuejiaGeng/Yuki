using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : MonoBehaviour
{
    public int maxHp = 3;
    public int hp = 3;

    public Transform lastAttacker;

    public RectTransform hpFill;
    public float fullWidth = 96f;

    private void Start()
    {
        hp = maxHp;
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("丧尸被打到了，伤害: " + damage);

        hp -= damage;

        if (hp < 0)
            hp = 0;

        UpdateHPBar();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            lastAttacker = playerObj.transform;

            if (GameSettings.Instance != null &&
                GameSettings.Instance.currentDifficulty == GameSettings.Difficulty.Hard)
            {
                GetComponent<ZombieCounterAttack>()?.CounterAttack(lastAttacker);
            }
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHPBar()
    {
        if (hpFill != null)
        {
            float percent = (float)hp / maxHp;
            hpFill.sizeDelta = new Vector2(fullWidth * percent, hpFill.sizeDelta.y);

            Debug.Log("丧尸当前血量: " + hp + " / " + maxHp);
        }
        else
        {
            Debug.LogWarning("HP_Fill 没有拖到 ZombieHealth 的 Hp Fill 上！");
        }
    }
}