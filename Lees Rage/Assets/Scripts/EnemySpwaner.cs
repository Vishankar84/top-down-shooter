using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPositionsFar;
    [SerializeField]
    private Transform[] spawnPositionsNear;

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject enemyLevel2;
    [SerializeField]
    private GameObject enemyLevel3;
    [SerializeField]
    private GameObject enemyLevel5;

    private float waitForNextSpawn;
    public float timeToSpawn;

    private float timeToSpawnEnemy2;
    private float timeToSpawnEnemy3;
    private float timeToSpawnEnemy5;

    private void Start()
    {
        waitForNextSpawn = 5;
    }

    private void Update()
    {
        if(!GameManager.Instance.gameOver) 
        {
            CalculateSpawnLevel();
            SpawnZombies();
            SpawnLeveledEnemies();
        }
    }

    private void SpawnLeveledEnemies()
    {
        if(GameManager.Instance.currentWave > 1)
        {
            float waitForNextSpawn = Random.Range(2, 10);

            if(timeToSpawnEnemy2 <= 0)
            {
                Instantiate(enemyLevel2, spawnPositionsFar[Random.Range(0, spawnPositionsFar.Length)].position, Quaternion.identity);
                timeToSpawnEnemy2 = waitForNextSpawn;
            }
            else
            {
                timeToSpawnEnemy2 -= Time.deltaTime;
            }
        }        
        if(GameManager.Instance.currentWave > 2)
        {
            float waitForNextSpawn = Random.Range(2, 10);

            if (timeToSpawnEnemy3 <= 0)
            {
                Instantiate(enemyLevel3, spawnPositionsNear[Random.Range(0, spawnPositionsNear.Length)].position, Quaternion.identity);
                timeToSpawnEnemy3 = waitForNextSpawn;
            }
            else
            {
                timeToSpawnEnemy3 -= Time.deltaTime;
            }
        }        
        if(GameManager.Instance.currentWave > 4)
        {
            float waitForNextSpawn = Random.Range(4, 10);

            if (timeToSpawnEnemy5 <= 0)
            {
                Instantiate(enemyLevel5, spawnPositionsFar[Random.Range(0, spawnPositionsFar.Length)].position, Quaternion.identity);
                timeToSpawnEnemy5 = waitForNextSpawn;
            }
            else
            {
                timeToSpawnEnemy5 -= Time.deltaTime;
            }
        }
    }

    private void CalculateSpawnLevel()
    {
        if(GameManager.Instance.totalKills >= GameManager.Instance.currentWave * 10)
        {
            if(GameManager.Instance.currentWave < GameManager.Instance.maxWaves)
            {
                GameManager.Instance.currentWave += 1;
                waitForNextSpawn -= 1;
                GameManager.Instance.ResetKills();
            }
        }        
    }

    private void SpawnZombies()
    {
        if (timeToSpawn <= 0)
        {
            GameObject newEnemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(newEnemy, spawnPositionsFar[Random.Range(0, spawnPositionsFar.Length)].position, Quaternion.identity);
            timeToSpawn = waitForNextSpawn * 0.5f;
        }
        else
        {
            timeToSpawn -= Time.deltaTime;
        }
    }
}
