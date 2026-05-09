using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public enum Difficulty
    {
        Easy,
        Hard
    }

    public Difficulty currentDifficulty = Difficulty.Easy;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleDifficulty()
    {
        if (currentDifficulty == Difficulty.Easy)
        {
            currentDifficulty = Difficulty.Hard;
        }
        else
        {
            currentDifficulty = Difficulty.Easy;
        }
    }
}