using System.Collections;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float swingAngle = 180f;     // 挥砍角度
    public float swingDuration = 0.12f; // 挥砍时间
    public float cooldown = 0.2f;       // 冷却，防止狂点

    private bool isSwinging = false;
    private float lastSwingTime = -999f;

    private Quaternion startRot;

    void Awake()
    {
        startRot = transform.localRotation;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left Click!");
            TrySwing();
        }
    }

    void TrySwing()
    {
        if (isSwinging) return;
        if (Time.time - lastSwingTime < cooldown) return;

        lastSwingTime = Time.time;
        StartCoroutine(SwingRoutine());
    }

    IEnumerator SwingRoutine()
    {
        isSwinging = true;

        // 目标旋转：在本地坐标绕Z轴转（2D像素一般用Z轴）
        Quaternion endRot = startRot * Quaternion.Euler(0, 0, -swingAngle);

        float t = 0f;
        while (t < swingDuration)
        {
            t += Time.deltaTime;
            float p = t / swingDuration;
            transform.localRotation = Quaternion.Slerp(startRot, endRot, p);
            yield return null;
        }

        // 回到初始角度
        t = 0f;
        while (t < swingDuration)
        {
            t += Time.deltaTime;
            float p = t / swingDuration;
            transform.localRotation = Quaternion.Slerp(endRot, startRot, p);
            yield return null;
        }

        transform.localRotation = startRot;
        isSwinging = false;
    }
}