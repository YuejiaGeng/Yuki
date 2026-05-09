using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 5;
    public int hp = 5;

    public Slider healthSlider;
    public DamageFlashUI damageFlashUI;
    public GameObject gameOverPanel;

    private bool isDead = false;

    private void Start()
    {
        hp = maxHp;
        isDead = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        hp -= damage;

        if (hp < 0)
            hp = 0;

        Debug.Log("玩家受伤，当前血量: " + hp);

        if (hp > 0 && damageFlashUI != null)
        {
            damageFlashUI.ShowFlash();
        }

        UpdateHealthUI();

        if (hp <= 0)
        {
            isDead = true;
            Debug.Log("玩家死亡");

            if (damageFlashUI != null)
            {
                damageFlashUI.gameObject.SetActive(false);
            }

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        hp += amount;

        if (hp > maxHp)
            hp = maxHp;

        Debug.Log("玩家回血，当前血量: " + hp);

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthSlider == null) return;

        healthSlider.wholeNumbers = true;
        healthSlider.maxValue = maxHp;
        healthSlider.value = hp;
    }
}