using UnityEngine;

public class DisplayScoreInGame : MonoBehaviour
{
    /*
        singleton pattern again
    */
    private static int instanceCount;
    public static DisplayScoreInGame selfInstance;

    void Awake()
    {
        instanceCount++;
        if (instanceCount > 1)
            return;
        selfInstance = this;
    }

    public void UpdateScore()
    {
        this.GetComponent<UnityEngine.UI.Text>().text = string.Format("Score {0}", Score.selfInstance.score);
    }
}
