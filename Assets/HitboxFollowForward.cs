using UnityEngine;

public class HitboxFollowForward : MonoBehaviour
{
    public Transform playerRoot;   // 拖玩家根物体（SA_Character_Scout）
    public float forward = 1.2f;   // 前方距离
    public float up = 1.0f;        // 高度（大概到胸口/手的高度）

    void LateUpdate()
    {
        if (playerRoot == null) return;

        // 放到玩家前方
        transform.position = playerRoot.position + playerRoot.forward * forward + Vector3.up * up;

        // 不旋转也行，保持触发球即可
        // transform.rotation = playerRoot.rotation;
    }
}