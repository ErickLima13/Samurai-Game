using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform[] positions;

    public int enemiesCount;
    public int waveNumber = 1;

    public GameObject enemyPrefab;

    private GameObject player;

    private void Initialization()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SpawnEnemyWave(waveNumber);
    }

    // Start is called before the first frame update
    void Start()
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
    }

    private void SpawnEnemies()
    {
        enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemiesCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        //int indexSpawnPoints = Random.Range(0, positions.Length);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        int indexSpawnPoints = Random.Range(0, positions.Length);

        float spawnPosX = Random.Range(-15, 30);
        float spawnPosY = Random.Range(-3f, -4.3f);

        Vector3 randomPos = new Vector3(positions[indexSpawnPoints].transform.position.x, spawnPosY, 0);

        return randomPos;
    }
}
