using UnityEngine;

public class RescueManager : MonoBehaviour
{
    public static RescueManager Instance;

    public int totalSurvivors = 6;
    public int rescuedSurvivors = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddRescuedSurvivor()
    {
        rescuedSurvivors++;

        if (rescuedSurvivors > totalSurvivors)
        {
            rescuedSurvivors = totalSurvivors;
        }

        Debug.Log("Rescued survivors: " + rescuedSurvivors + " / " + totalSurvivors);
    }

    public bool AllSurvivorsRescued()
    {
        return rescuedSurvivors >= totalSurvivors;
    }
}