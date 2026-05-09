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

        if (ui == null)
        {
            Debug.LogWarning("SafeZoneTrigger Ã»ÓÐ°ó¶¨ ResultUI£¡");
            return;
        }

        if (RescueManager.Instance != null && RescueManager.Instance.AllSurvivorsRescued())
        {
            Debug.Log("Game Victory! All survivors rescued.");
            ui.ShowVictory();
        }
        else
        {
            Debug.Log("Game Over! Not all survivors rescued.");
            ui.ShowGameOver();
        }
    }
}