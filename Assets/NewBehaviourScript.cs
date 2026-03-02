using UnityEngine;

public class CameraFollowPositionOnly : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 6, -8);
    public float followSpeed = 10f;

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // 关键：不要 LookAt，不要跟着人物转
        // transform.LookAt(target);  // ❌ 永远不要写这行
    }
}