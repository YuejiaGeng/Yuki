using UnityEngine;

public class ArmSwing : MonoBehaviour
{
    public Transform forearm;
    public float swingAngle = 120f;
    public float swingSpeed = 10f;
    public float cooldown = 0.25f;

    public AttackHitbox hitbox;
    public WeaponSwitcher weaponSwitcher;

    private bool swinging = false;
    private float t = 0f;
    private float lastTime = -999f;

    void Start()
    {
        if (weaponSwitcher == null)
        {
            weaponSwitcher = GetComponent<WeaponSwitcher>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastTime < cooldown) return;

            lastTime = Time.time;

            swinging = true;
            t = 0f;

            // 弩模式下：只做动作，不造成近战伤害
            if (weaponSwitcher != null && weaponSwitcher.usingCrossbow)
            {
                return;
            }

            // 近战模式下：才真正打丧尸
            if (hitbox != null)
            {
                hitbox.DoHitOnce();
            }
        }
    }

    void LateUpdate()
    {
        if (!swinging || forearm == null) return;

        t += Time.deltaTime * swingSpeed;

        float angle = Mathf.Sin(t * Mathf.PI) * swingAngle;
        forearm.localRotation = Quaternion.Euler(-angle, 0, 0);

        if (t >= 1f)
        {
            swinging = false;
            forearm.localRotation = Quaternion.identity;
        }
    }
}