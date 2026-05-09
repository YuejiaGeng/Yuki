using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CrossbowShoot : MonoBehaviour
{
    [Header("Shoot")]
    public Transform shootPoint;
    public float range = 15f;
    public float shootAngle = 35f;   // 前方扇形角度，越大越容易打中
    public int damage = 1;
    public LayerMask targetLayers;

    [Header("Ammo")]
    public int maxAmmo = 5;
    public int currentAmmo = 5;
    public TMP_Text ammoText;
    public TMP_Text usedUpText;

    [Header("Visual")]
    public GameObject loadedArrow;
    public float reloadTime = 0.25f;

    private WeaponSwitcher weaponSwitcher;
    private bool isReloading = false;

    private void Start()
    {
        weaponSwitcher = GetComponent<WeaponSwitcher>();

        currentAmmo = maxAmmo;
        UpdateAmmoUI();

        if (loadedArrow != null)
            loadedArrow.SetActive(true);

        if (usedUpText != null)
            usedUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (weaponSwitcher == null) return;

        if (weaponSwitcher.usingCrossbow && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            Debug.Log("弩箭用完了");

            if (usedUpText != null)
            {
                StopAllCoroutines();
                StartCoroutine(ShowUsedUpText());
            }

            return;
        }

        currentAmmo--;
        UpdateAmmoUI();

        StartCoroutine(ShootVisual());

        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Collider[] hits = Physics.OverlapSphere(origin, range, targetLayers, QueryTriggerInteraction.Ignore);

        ZombieHealth closestZombie = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            ZombieHealth zombie = hit.GetComponentInParent<ZombieHealth>();
            if (zombie == null) continue;

            Vector3 directionToZombie = zombie.transform.position - origin;
            directionToZombie.y = 0f;

            float distance = directionToZombie.magnitude;
            if (distance <= 0.01f) continue;

            directionToZombie.Normalize();

            float angle = Vector3.Angle(forward, directionToZombie);

            if (angle <= shootAngle)
            {
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestZombie = zombie;
                }
            }
        }

        if (closestZombie != null)
        {
            Debug.Log("弩射中了：" + closestZombie.name);
            closestZombie.TakeDamage(damage);
        }
        else
        {
            Debug.Log("弩没有射中：前方范围内没有丧尸");
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
    }

    IEnumerator ShootVisual()
    {
        isReloading = true;

        if (loadedArrow != null)
            loadedArrow.SetActive(false);

        yield return new WaitForSeconds(reloadTime);

        if (loadedArrow != null && currentAmmo > 0)
            loadedArrow.SetActive(true);

        isReloading = false;
    }

    IEnumerator ShowUsedUpText()
    {
        if (usedUpText == null) yield break;

        usedUpText.gameObject.SetActive(true);
        usedUpText.text = "Used up!";

        yield return new WaitForSeconds(1f);

        usedUpText.gameObject.SetActive(false);
    }
}