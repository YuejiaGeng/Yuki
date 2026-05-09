using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI difficultyText;

    private void Start()
    {
        UpdateDifficultyText();
    }

    public void OnClickComplexity()
    {
        if (GameSettings.Instance != null)
        {
            GameSettings.Instance.ToggleDifficulty();
        }

        UpdateDifficultyText();
    }

    void UpdateDifficultyText()
    {
        if (GameSettings.Instance != null)
        {
            if (GameSettings.Instance.currentDifficulty == GameSettings.Difficulty.Hard)
            {
                difficultyText.text = "Complexity: Hard";
            }
            else
            {
                difficultyText.text = "Complexity: Easy";
            }
        }
    }
}