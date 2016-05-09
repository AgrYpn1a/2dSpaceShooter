using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // number of scores we want to display
    public static string[] scores = { "score1", "score2", "score3" };

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
