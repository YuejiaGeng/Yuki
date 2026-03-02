using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerAutoForwardCC : MonoBehaviour
{
    public float speed = 2f;      // 向前走的速度
    public float gravity = -20f;  // 重力

    private CharacterController cc;
    private Vector3 velocity;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Debug.Log("AUTO FORWARD RUNNING: " + gameObject.name);
    }

    void Update()
    {
        // 1) 永远向前（用角色自己的 forward）
        Vector3 move = transform.forward * speed;

        // 2) 重力
        if (cc.isGrounded && velocity.y < 0f)
            velocity.y = -2f; // 贴地，别飘
        velocity.y += gravity * Time.deltaTime;

        // 3) 合并移动
        Vector3 finalMove = move + velocity;
        cc.Move(finalMove * Time.deltaTime);
    }
}