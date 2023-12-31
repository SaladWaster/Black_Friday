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
        public int waveQuota;   // Total no. of enemies to spawn in this wave
        public float spawnInterval; // Time interval to spawn enemies
        public int spawnCount;  // No. of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;  // No. of enemies to spawn in this wave
        public int spawnCount;  // No. of enemies already spawned in this wave
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;    // List of all waves
    public int currentWaveCount;    // current wave index


    [Header("Spawn Attributes")]
    float spawnTimer; // Timer for when to spawn the next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed; // The maximum number of enemies allowed on the map at once
    public bool maxEnemiesReached = false;
    public float waveInterval; // Time interval between each wave


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
        // If current wave count is still less than the total number of waves & the current wave has no more spawns left
        // AND If the wave is no longer active, start the next wave
        if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive)
        {
            StartCoroutine(StartNextWave());
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

    IEnumerator StartNextWave()
    {
        isWaveActive = true;

        // Wait for the wave time interval before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        // If there ar emore waves to start after the current (Checks index)
        // -1 due to index starting from 0
        // What this does atm is...
        // Checks if the currentWaveCount (Index) is less than that of the last wave's index
        // This seems to be the source of the problem.
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
            // counts every enemy we have placed in the enemyGroup
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
        // Check to see if min number of enemies for the current wave are spawned
        // If not, continue to spawn current wave until max on screen reaches
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {

            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                // Check if min number of enemies for this type has been spawned
                // If less than the count for the wave, continue to spawn
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    

                    // // Choose a random relative spawn point
                    // Vector3 spawnPosition = player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position;

                    // Spawn the enemy at a random position
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);
                    
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
