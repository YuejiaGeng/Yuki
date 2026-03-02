using UnityEngine;

public class ZombieCounter : MonoBehaviour
{
    private ResultUI ui;
    private bool counted;

    private void Start()
    {
        ui = Object.FindObjectOfType<ResultUI>();
        if (ui == null) return;

        ui.aliveZombies++;
        counted = true;
    }

    private void OnDestroy()
    {
        if (ui == null || !counted) return;

        ui.aliveZombies--;
        if (ui.aliveZombies < 0) ui.aliveZombies = 0;
    }
}