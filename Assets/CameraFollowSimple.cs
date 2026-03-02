using UnityEngine;

public class CameraFollowSimple : MonoBehaviour
{
    public Transform target;

    // 相机相对角色的固定偏移（第三人称常用）
    public Vector3 offset = new Vector3(0f, 6f, -8f);

    // 跟随平滑
    public float followSpeed = 10f;

    void LateUpdate()
    {
        if (!target) return;

        // 只跟随位置：目标位置 = 角色位置 + 固定偏移（世界坐标）
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // 关键：先不要自动 LookAt！不然又会开始“追着转”
        // transform.LookAt(target);
    }
}