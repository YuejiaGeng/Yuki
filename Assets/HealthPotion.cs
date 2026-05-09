using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            playerHealth = other.GetComponentInParent<PlayerHealth>();
        }

        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Debug.Log("拾取补给品，恢复生命：" + healAmount);

            Destroy(gameObject);
        }
    }
}