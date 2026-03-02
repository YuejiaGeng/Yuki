using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;   // 场景切换

public class ResultUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TextMeshProUGUI text;

    [Header("Game State")]
    public int aliveZombies = 0;

    // ===== 胜利 =====
    public void ShowVictory()
    {
        panel.SetActive(true);
        text.text = "Game victory";
        Time.timeScale = 0f;
    }

    // ===== 失败 =====
    public void ShowGameOver()
    {
        panel.SetActive(true);
        text.text = "Game over";
        Time.timeScale = 0f;
    }

    // ===== Restart 按钮 =====
    public void RestartGame()
    {
        Time.timeScale = 1f; // 恢复时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ===== Main Menu 按钮 =====
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // 恢复时间
        SceneManager.LoadScene("mainmenu"); // ← 主菜单场景名
    }

    // 兼容旧函数
    public void ShowWin() => ShowVictory();
    public void ShowLose() => ShowGameOver();
}