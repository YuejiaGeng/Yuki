using UnityEngine;

public class RankingUI : MonoBehaviour
{
    public GameObject rankingPanel;

    private void Start()
    {
        if (rankingPanel != null)
        {
            rankingPanel.SetActive(false);
        }
    }

    public void ShowRanking()
    {
        if (rankingPanel != null)
        {
            rankingPanel.SetActive(true);
        }
    }

    public void HideRanking()
    {
        if (rankingPanel != null)
        {
            rankingPanel.SetActive(false);
        }
    }
}