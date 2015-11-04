using UnityEngine;

[System.Serializable]
public struct EnemyBoundaries
{
    public float top, bottom, left, right;
};

public abstract class IEnemyAI : MonoBehaviour
{
    // this is just an enemy behaviour interface
    public float enemyHitPoints;
    public GameObject enemyWeapon;
    public Transform enemyWeaponHp;
    public int enemyWeaponSpeed;

    public int fireRateMax;
    public int fireRateMin;

    public ParticleSystem deathPart;

    protected bool isDead;

    // we need to restrict enemy movement as well
    public EnemyBoundaries enemyBoundaries;

    public abstract void EnemyBehaviour();
    public abstract void ApplyDamage();
    public abstract void Death();
}
