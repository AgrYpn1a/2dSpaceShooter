using UnityEngine;
using System.Collections;
using System;

public class KerbanautInvader : IEnemyAI
{
    private Vector2 nextMove = Vector2.zero;

    // helper variables needed in order to do correct lerping
    private bool isLerping = false;
    private Vector2 oldPos;
    private float timeSinceStarted;
    private static float currentLerpPercentage;

    // shooting
    private float nextShot;
    private int fireRate;

    void Start()
    {
        this.nextShot = Time.time + UnityEngine.Random.Range(2, 4);
        this.initializeLerp();
    }

    void Update()
    {
        if (this.isDead)
            return;
        this.EnemyBehaviour();
    }

    public override void EnemyBehaviour()
    {
        /*
            The basic idea, is to generate a random vector each [time interval in seconds]
            and move the enemy from old position to a new one.
            
            Now problem is, that if we generate too high a number, enemy will leave the screen.
            In order to fix this, we will generate a number within movement boundaries, set via inspector

            Now we have to smooth out the transition between positions, and there is a method called Lerp
            which will allow us to do so.
        */

        if (isLerping)
        {
            // we saved the time when we started lerping, and now in order to get the time difference
            // we will substract old time from new time
            // Time.time - oldTime, when this becomes 1 (100%), lerp is done
            currentLerpPercentage = Time.time - this.timeSinceStarted;
            if (currentLerpPercentage >= 1)
            {
                this.isLerping = false;
                this.initializeLerp();
                return;
            }
            this.transform.position = Vector2.Lerp(this.oldPos, this.nextMove, currentLerpPercentage / 2);
        }

        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            fireShot();
        }

        fireRate = UnityEngine.Random.Range(fireRateMin, fireRateMax);
    }

    private void initializeLerp()
    {
        this.oldPos = this.transform.position;
        // Time.time measures the Time since game started
        this.timeSinceStarted = Time.time;
        this.nextMove = this.generateNextMove();

        this.isLerping = true;
    }

    private Vector3 generateNextMove()
    {
        // generate nextMove vector relative to the boundaries
        return new Vector2(UnityEngine.Random.Range(this.enemyBoundaries.left, this.enemyBoundaries.right),
            UnityEngine.Random.Range(this.enemyBoundaries.top, this.enemyBoundaries.bottom));
    }

    public override void ApplyDamage()
    {
        if (this.isDead)
            return;
        this.enemyHitPoints -= GlobalStats.instance.playerDamage;
        this.GetComponent<Animator>().SetTrigger("hit");
        Death();
    }

    // when enemy dies, before we destroy it, we want to make sure that our player's score
    // has increased by a value set in GlobalStats class
    public override void Death()
    {
        if (this.enemyHitPoints <= 0)
        {
            this.isDead = true;
            Score.selfInstance.score += GlobalStats.instance.kerbanautKillScore;
            DisplayScoreInGame.selfInstance.UpdateScore();
            this.deathPart.gameObject.SetActive(true);
            Destroy(this.gameObject, this.deathPart.duration);
        }
    }

    private void fireShot()
    {
        GameObject temp = Instantiate(this.enemyWeapon, enemyWeaponHp.position, Quaternion.identity) as GameObject;
        temp.GetComponent<Rigidbody2D>().AddForce(-transform.up * enemyWeaponSpeed);
    }
}
