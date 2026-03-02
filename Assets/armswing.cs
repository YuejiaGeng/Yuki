using UnityEngine;

public class ArmSwing : MonoBehaviour
{
    public Transform forearm;
    public float swingAngle = 120f;
    public float swingSpeed = 10f;
    public float cooldown = 0.25f;

    public AttackHitbox hitbox;

    private bool swinging = false;
    private float t = 0f;
    private float lastTime = -999f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastTime < cooldown) return;
            lastTime = Time.time;

            swinging = true;
            t = 0f;

            if (hitbox != null) hitbox.DoHitOnce();
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