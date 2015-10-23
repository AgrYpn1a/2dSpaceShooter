using UnityEngine;

[System.Serializable]
public struct EnemyBoundaries
{
    public float top, bottom, left, right;
};

public abstract class IEnemyAI : MonoBehaviour
{
    // this is just an enemy behaviour interface
    public float enemySpeed;
    public int minMoveDistance;
    public int maxMoveDistance;
    public float enemyHitPoints;
    public GameObject enemyWeapon;
    // we need to restrict enemy movement as well
    public EnemyBoundaries enemyBoundaries;

    public abstract void EnemyBehaviour();
    public abstract void ApplyDamage();
}
