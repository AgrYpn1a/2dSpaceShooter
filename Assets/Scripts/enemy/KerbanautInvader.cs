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

    void Start()
    {
        initializeLerp();
    }

    void Update()
    {
        this.EnemyBehaviour();
    }

    public override void EnemyBehaviour()
    {
        /*
            The basic idea, is to generate a random vector each [time interval in seconds]
            and let the enemy move from its old position to the new position,
            
            Now problem is, that if we generate too high a number, our enemy will leave the screen.
            In order to fix this, we will generate a number within movement boundaries

            We also want to make sure that enemy moves at least a visible distance, which we will 
            ensure by setting the minimum distance between old and new position
        */

        /*
            Now we have to smooth out the transition between positions, and there is a method called Lerp
            which will allow us to do so,
            There is an easy way to make this work by simply tell current position to lerp from old to new : 
            this.transform.position = Vector3.Lerp(this.transform.position, nextMove, Time.deltaTime);
            However, this will result in inconsistent speed and make the transitions look ugly, with suden slow downs
            This will happen because, as third parameter of Lerp() method, we have to pass the current percentage of
            transition, but we're passing deltaTime instead, which is a small number and will never reach 1
            in the other words, it will never allow lerp to finish
        */

        if (isLerping)
        {
            // we saved the time when we started lerping, and now in order to get the time difference
            // we will substract old time from new time, and when that rises from 0 to 1, we will
            // have our lerp finished
            currentLerpPercentage = Time.time - this.timeSinceStarted;
            if (currentLerpPercentage >= 1)
            {
                isLerping = false;
                initializeLerp();
                return;
            }
            this.transform.position = Vector2.Lerp(this.oldPos, this.nextMove, currentLerpPercentage / 2);
        }
    }

    private void initializeLerp()
    {
        this.oldPos = this.transform.position;
        // Time.time measures the Time since game started
        this.timeSinceStarted = Time.time;
        this.nextMove = this.generateNextMove();

        // check if nextMove meets requierements
        //while ((nextMove - oldPos).sqrMagnitude < this.minMoveDistance && (nextMove - oldPos).sqrMagnitude > this.maxMoveDistance)
        //{
            this.nextMove = generateNextMove();
        //}

        this.isLerping = true;
    }

    private Vector3 generateNextMove()
    {
        Vector3 temp;

        // generate nextMove relative to the boundaries
        temp = new Vector2(UnityEngine.Random.Range(this.enemyBoundaries.left, this.enemyBoundaries.right),
            UnityEngine.Random.Range(this.enemyBoundaries.top, this.enemyBoundaries.bottom));

        return temp;
    }

    public override void ApplyDamage()
    {
        this.enemyHitPoints -= GlobalStats.instance.playerDamage;
        this.GetComponent<Animator>().SetTrigger("hit");
        if (this.enemyHitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
