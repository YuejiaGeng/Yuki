using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementCC : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2.5f;
    public float runSpeed = 4.5f;
    public float gravity = -20f;

    [Header("Rotation (turn to move direction)")]
    public float turnSmoothSpeed = 12f;

    [Header("Attack")]
    public float attackCooldown = 0.25f;

    private CharacterController cc;
    private Vector3 verticalVel;

    private Animator animator;

    // walk 参数
    private int walkParamHash = -1;
    private bool hasWalkParam = false;

    // attack 参数（Trigger）
    private int attackParamHash = -1;
    private bool hasAttackParam = false;

    private float lastAttackTime = -999f;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        if (animator == null)
        {
            Debug.LogError("No Animator found on player or its children!");
            return;
        }

        // 自动找 walk 参数：优先 isWalk，其次 is walk
        hasWalkParam = TryGetParamHash(animator, "isWalk", out walkParamHash)
                    || TryGetParamHash(animator, "is walk", out walkParamHash);

        // 自动找 attack Trigger：优先 Attack，其次 attack
        hasAttackParam = TryGetParamHash(animator, "Attack", out attackParamHash)
                      || TryGetParamHash(animator, "attack", out attackParamHash);

        Debug.Log("Animator on object: " + animator.gameObject.name);
        Debug.Log("Controller: " + (animator.runtimeAnimatorController ? animator.runtimeAnimatorController.name : "NULL"));
        Debug.Log("Has walk param? " + hasWalkParam);
        Debug.Log("Has attack trigger? " + hasAttackParam + (hasAttackParam ? "" : "  <-- 去Animator里加 Trigger 参数：Attack"));
    }

    void Update()
    {
        // ===== 攻击：左键触发 Attack Trigger =====
        if (Input.GetMouseButtonDown(0))
        {
            if (animator != null && hasAttackParam && Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;
                animator.SetTrigger(attackParamHash);
            }
        }

        // ===== 移动输入 =====
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(x, 0f, z);
        input = Vector3.ClampMagnitude(input, 1f);

        bool isMoving = input.sqrMagnitude > 0.0001f;

        // walk 动画切换
        if (animator != null && hasWalkParam)
        {
            animator.SetBool(walkParamHash, isMoving);
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Transform cam = Camera.main ? Camera.main.transform : null;

        Vector3 moveDir;
        if (cam != null)
        {
            Vector3 camForward = cam.forward; camForward.y = 0f; camForward.Normalize();
            Vector3 camRight = cam.right; camRight.y = 0f; camRight.Normalize();
            moveDir = camRight * input.x + camForward * input.z;
        }
        else
        {
            moveDir = new Vector3(input.x, 0f, input.z);
        }

        if (moveDir.sqrMagnitude > 0.0001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSmoothSpeed * Time.deltaTime);
        }

        if (cc.isGrounded && verticalVel.y < 0f) verticalVel.y = -2f;
        verticalVel.y += gravity * Time.deltaTime;

        Vector3 finalMove = moveDir * speed + verticalVel;
        cc.Move(finalMove * Time.deltaTime);
    }

    private bool TryGetParamHash(Animator a, string paramName, out int hash)
    {
        foreach (var p in a.parameters)
        {
            if (p.name == paramName)
            {
                hash = Animator.StringToHash(paramName);
                return true;
            }
        }
        hash = -1;
        return false;
    }
}