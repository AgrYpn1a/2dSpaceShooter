using UnityEngine;
using System.Collections;

public class GlobalStats : MonoBehaviour
{
    /*
        This class will hold some global information that won't change throughout game
        such as player/enemy damage and more

    */
    public int playerDamage;
    public int enemyDamage;
    public int kerbanautKillScore;

    // beccause this class inherits Monobehaviour it cannot be static.
    // In order to achieve this, we have to use pattern called Singleton
    // which will allow us to immitate static class, by restricting the number
    // of instances to 1

    public static GlobalStats instance = null;
    private static int instanceCount;

    void Awake()
    {
        instanceCount++;
        if (instanceCount > 1)
            return;
        instance = this;
    }
}
