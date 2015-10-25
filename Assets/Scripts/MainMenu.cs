using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // number of scores we want to display
    public static string[] scores = { "score1", "score2", "score3" };

    void Awake()
    {
        // initialize values in player prefs (more details about player prefs in class Score)
        foreach(string score in scores)
        {
            if (PlayerPrefs.GetInt(score) <= 0)
            {
                PlayerPrefs.SetInt(score, 0);
            }
        }
    }

    public void StartGame()
    {
        Application.LoadLevel("game");
    }

    public void Score()
    {
        Application.LoadLevel("score");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
