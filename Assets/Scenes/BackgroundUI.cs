using UnityEngine;

public class BackgroundUI : MonoBehaviour
{
    public GameObject backgroundPanel;

    private void Start()
    {
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }

    public void ShowBackground()
    {
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(true);
        }
    }

    public void HideBackground()
    {
        if (backgroundPanel != null)
        {
            backgroundPanel.SetActive(false);
        }
    }
}