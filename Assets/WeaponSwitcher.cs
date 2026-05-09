using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject meleeHitbox;       // 近战攻击范围
    public GameObject meleeWeapon;       // 近战武器模型
    public GameObject crossbowWeapon;    // 弩模型

    public bool usingCrossbow = false;

    private void Start()
    {
        SetWeapon(false); // 默认近战
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            usingCrossbow = !usingCrossbow;
            SetWeapon(usingCrossbow);
        }
    }

    void SetWeapon(bool useCrossbow)
    {
        if (crossbowWeapon != null)
            crossbowWeapon.SetActive(useCrossbow);

        if (meleeHitbox != null)
            meleeHitbox.SetActive(!useCrossbow);

        if (meleeWeapon != null)
            meleeWeapon.SetActive(!useCrossbow);

        if (useCrossbow)
            Debug.Log("切换到远程武器：弩");
        else
            Debug.Log("切换到近战武器");
    }
}