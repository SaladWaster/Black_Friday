using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups;    // List of enemy types
        public int waveQuota;   // No. of enemies to spawn
        public float spawnInterval; // Time interval to spawn enemies
        public int spawnCount;  // No. of enemies already in wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;  // No. of enemies to spawn in wave
        public int spawnCount;  // No. of enemies already in wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;


    [Header("Spawn Attributes")]
    float spawnTimer; // Timer for when to spawn the next enemy
    public float waveInterval; // Interval between each wave
    public int enemiesAlive;
    public int maxEnemiesAllowed; // The maximum number of enemies allowed on the map at once
    public bool maxEnemiesReached = false;
    bool isWaveActive = false;

    // // Define the range of spawning positions
    // public Transform minSpawn, maxSpawn;

    // List of relative spawn points to use when spawning enemies
    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnPoints;

    Transform player;

    void Start()
    {
        // Find the player's transform to use it for relative spawning
        player = FindObjectOfType<PlayerStats>().transform;

        // Calculate the enemy quota for the current wave
        CalculateWaveQuota();
    }

    void Update()
    {
        // Check if it's time to start the next wave
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(NextWave());
        }

        // Update the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn enemies
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator NextWave()
    {
        isWaveActive = true;

        // Wait for the wave interval before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        // Check if there are more waves to start
        // -1 due to index starting from 0
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota()
    {
        // Calculate the total enemy quota for the current wave
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        // Update the wave's quota
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        //Debug.LogWarning(currentWaveQuota);
    }


    /// <summary>
    ///  Stop spawning if amount of enemies on map is maxed
    ///  This method will only spawn for a particular wave until next wave spawning begins
    /// </summary>
    void SpawnEnemies()
    {
        // CHeck to see if min number of enemies for wave are spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Check if min number for an enemy group is reached
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    

                    // Choose a random relative spawn point
                    Vector3 spawnPosition = player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;

                    // Spawn the enemy at the chosen position
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;

                    // Limit the number of enemies that can be spawned at once
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                }
            }
        }

    }

    public void OnEnemyDefeated()
    {
        enemiesAlive--;

        // Reset the maxEnemiesReached flag if there are fewer enemies alive
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }
}
