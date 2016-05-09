using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefs : MonoBehaviour
{
    [MenuItem("Other/Reset Player Prefs")]
    static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
