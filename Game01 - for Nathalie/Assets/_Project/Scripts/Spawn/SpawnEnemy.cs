using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] positions;

    public float radius;

    public int enemiesCount;
    public int waveNumber = 1;
    public int bossCount;

    public bool isBoss;

   

    public GameObject bossPrefab;

    private GameObject player;
    private GameObject enemyPrefab;

    private void Initialization()
    {
        enemyPrefab = Resources.Load<GameObject>("Flight_enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemyWave(waveNumber);
    }

    void Awake()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<Player>().isDead)
        {
            SpawnEnemies();   
        }

        if (!isBoss)
        {
            SpawnBoss();
        }
    }

    private void SpawnEnemies()
    {
        enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesCount == 0 && !isBoss)
        {
            waveNumber++;
            
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), transform.rotation);
        }
    }

    private void SpawnBoss()
    {
        if(waveNumber % 2 == 0) // colocar divisor de 5 cinco apos fazer evento;
        {
            bossCount += 10;
            player.GetComponent<Player>().damage++;
            isBoss = true;
            bossPrefab.SetActive(true);

            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

            for (var i = 0; i < gameObjects.Length; i++)
            {
                Destroy(gameObjects[i]);
            }
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        int indexSpawnPoints = Random.Range(0, positions.Length);

        float spawnPosX = Random.Range(-20, 30);
        float spawnPosY = Random.Range(-3f, -4.10f);

        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 0);

        return randomPos;
    }
}
