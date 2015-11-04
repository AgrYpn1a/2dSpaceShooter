using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private int pauseBetweenWaves;
    [SerializeField]
    private Transform enemyHolder;
    [SerializeField]
    private GameObject nextWave;
    [SerializeField]
    private IEnemyAI enemy;

    private float checkRate = 0f;
    private float checkSpawnRate;

    private int currentEnemySpawnCount = 1;

    void Update()
    {
        /*
        if (Time.time > checkRate)
        {
            checkRate = Time.time + 1f;
            if (enemyHolder.childCount == 0)
            {
                // spawn new enemies
                this.nextWave.SetActive(true);
                this.spawnEnemies();
            }
        }
        */
        if (this.enemyHolder.childCount == 0)
        {
            this.nextWave.SetActive(true);
            StartCoroutine(this.delaySpawn());
            //this.spawnEnemies();
        }

    }

    private void spawnEnemies()
    {
        if (this.enemyHolder.childCount == 0)
        {
            for (int i = 0; i < currentEnemySpawnCount; i++)
            {
                IEnemyAI temp = Instantiate(this.enemy, this.spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position, Quaternion.identity) as IEnemyAI;
                temp.transform.parent = this.enemyHolder;
            }
            this.nextWave.SetActive(false);
            this.currentEnemySpawnCount++;

        }
    }

    private IEnumerator delaySpawn()
    {
        yield return new WaitForSeconds(this.pauseBetweenWaves);
        this.spawnEnemies();
    }
}
