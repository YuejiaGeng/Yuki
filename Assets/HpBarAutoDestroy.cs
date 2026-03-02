using UnityEngine;

public class HpBarAutoDestroy : MonoBehaviour
{
    // 血条跟随的目标（丧尸/敌人）
    public Transform target;

    void LateUpdate()
    {
        // 目标被销毁 或 被SetActive(false) 时，自动销毁血条
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
    }
}