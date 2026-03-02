using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Image fill;   // ÍÏ UP_Fill µÄ Image ½øÀ´
    public int maxHits = 3;
    private int hits = 0;

    public void TakeHit()
    {
        hits++;
        float remain = (maxHits - hits) / (float)maxHits;
        fill.fillAmount = remain;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeHit();
        }
    }
}