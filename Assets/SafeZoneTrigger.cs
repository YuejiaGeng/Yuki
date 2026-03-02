using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    public ResultUI ui;
    public string playerTag = "Player";
    private bool triggered = false;

    private void OnTriggerStay(Collider other)
    {
        if (triggered) return;

        Debug.Log("Stay SafeZone: " + other.name);

        if (!other.CompareTag(playerTag)) return;

        triggered = true;

        if (ui != null && ui.aliveZombies <= 0)
            ui.ShowVictory();
        else
            ui.ShowGameOver();
    }
}