using UnityEngine;
using System;
using System.Linq;

public class Score : MonoBehaviour
{
    /*
        Singleton pattern class again, because we want an immitation of static class
        so that we can easily access Score.score from anywhere
    */

    /*
        This class will handle scores saving via PlayerPrefs
        In PlayerPrefs we can store any values we want to persist
        even though game has quit
        We could use external database for this, but since we 
        only want to save couple of scores and display them
        it is certainly not worth having a database
    */
    public static Score selfInstance;
    static int instanceCount;

    public int score = 0;

    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private GameObject scoreObject = null;
    [SerializeField]
    private Vector2 scorePosition = Vector2.one;

    void Awake()
    {
        instanceCount++;
        if (instanceCount > 1)
            return;
        selfInstance = this;
    }

    void Start()
    {
        // if "score" scene has loaded we want to automatically display score
        if (Application.loadedLevelName == "score")
        {
            this.displayScore();
        }
    }

    // this method will be called each time player dies
    // and will save current score
    public void SaveScore()
    {
        // we save current score minimum
        string min = getMinScoreIndex();
        int[] scores = new int[MainMenu.scores.Length];
        int counter = 0;

        // we replace current minimum score with current score
        PlayerPrefs.SetInt(min, this.score);
        scores = makeScoreArray();
        // sorting array to be able to display scores in correct order
        scores = (from i in scores orderby i descending select i).ToArray();

        foreach (string score in MainMenu.scores)
        {
            PlayerPrefs.SetInt(score, scores[counter]);
            counter++;
        }
    }

    // get scores from player prefs, and set them in an array
    private int[] makeScoreArray()
    {
        int[] temp = new int[MainMenu.scores.Length];
        int counter = 0;

        foreach (string score in MainMenu.scores)
        {
            temp[counter] = PlayerPrefs.GetInt(score);
            counter++;
        }

        return temp;
    }

    private string getMinScoreIndex()
    {
        string min = MainMenu.scores[0];

        foreach (string score in MainMenu.scores)
        {
            if (PlayerPrefs.GetInt(score) < PlayerPrefs.GetInt(min))
            {
                min = score;
            }
        }
        return min;
    }

    private void displayScore()
    {
        int counter = 1;

        foreach (string score in MainMenu.scores)
        {
            GameObject temp = Instantiate(scoreObject, scorePosition, Quaternion.identity) as GameObject;
            temp.transform.SetParent(canvas.transform);
            temp.transform.localScale = Vector2.one;
            temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.scorePosition.x, this.scorePosition.y - (counter * 8));
            temp.GetComponent<UnityEngine.UI.Text>().text = String.Format("{0}. {1:N}", counter, PlayerPrefs.GetInt(score));
            counter++;
        }
    }
}
