using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 8f;
    public float height = 0f;          // 现在用 pitch 控制上下视角，height 可以设 0 或很小
    public float followSpeed = 10f;

    public float rotationSpeedX = 120f; // 左右
    public float rotationSpeedY = 120f; // 上下

    public float minPitch = -20f;       // 最低能看多低
    public float maxPitch = 60f;        // 最高能看多高

    float yaw = 0f;
    float pitch = 20f;                 // 初始俯仰角（你可以调）

    void LateUpdate()
    {
        if (!target) return;

        // 鼠标控制
        yaw += Input.GetAxis("Mouse X") * rotationSpeedX * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeedY * Time.deltaTime; // 注意这里是 -=，鼠标上移就是抬头
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // 旋转（先俯仰再偏航）
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // 相机位置：围绕 target 的后方一定距离
        Vector3 desiredPosition = target.position
                                  - rotation * Vector3.forward * distance
                                  + Vector3.up * height;

        // 平滑跟随
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // 看向角色（略抬高一点看胸口/头部）
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}