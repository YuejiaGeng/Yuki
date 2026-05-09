using UnityEngine;

public class TentRescue : MonoBehaviour
{
    public GameObject npcInTent;          // 这个帐篷里的幸存者
    public Transform safeZonePoint;       // 幸存者被救后去的位置
    public GameObject requiredZombie;     // 必须先击败的丧尸

    public Transform player;
    public float rescueDistance = 3f;

    private bool rescued = false;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    private void Update()
    {
        if (rescued) return;
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= rescueDistance && Input.GetKeyDown(KeyCode.F))
        {
            TryRescueNPC();
        }
    }

    void TryRescueNPC()
    {
        if (requiredZombie != null)
        {
            Debug.Log("Please defeat the zombie near this tent first!");
            return;
        }

        RescueNPC();
    }

    void RescueNPC()
    {
        if (npcInTent == null)
        {
            Debug.LogWarning("Npc In Tent 没有拖！");
            return;
        }

        if (safeZonePoint == null)
        {
            Debug.LogWarning("Safe Zone Point 没有拖！");
            return;
        }

        npcInTent.transform.position = safeZonePoint.position;
        npcInTent.transform.rotation = safeZonePoint.rotation;

        rescued = true;

        if (RescueManager.Instance != null)
        {
            RescueManager.Instance.AddRescuedSurvivor();
        }
        else
        {
            Debug.LogWarning("场景里没有 RescueManager！");
        }

        Debug.Log("Survivor rescued!");
    }
}