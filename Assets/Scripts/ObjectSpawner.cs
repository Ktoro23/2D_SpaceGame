using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

    // --- NEW: A public reference to the ObjectSpawner to access it from CoinPath ---
    public static ObjectSpawner Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    [System.Serializable]
    public class Wave
    {

        public ObjectPooler pool;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnObjectCount;

        // --- NEW: Used to ensure coins spawn sequentially for a clean line/shape ---
        public int coinSpawnCount;

        [Header("Coin Wave Settings")]
        [Tooltip("If checked, coins will spawn in a perfect line for wave patterns.")]
        public bool isCoinWave;

        // --- NEW: Variables to store the single, randomized starting offset for the entire wave ---
        [HideInInspector] public float waveStartJitterY;
        [HideInInspector] public float waveStartJitterX;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if GameManger is active before accessing its members
        if (GameManger.Instance == null) return;

        // --- CRITICAL FIX: USE BASE SPEED FOR TIMER ---
        // Instead of adjustedworldSpeed (which includes the boost factor),
        // we use the constant baseScrollSpeed * Time.deltaTime. 
        // This ensures the spawn rate never changes, regardless of the boost.
        waves[waveNumber].spawnTimer -= GameManger.Instance.baseScrollSpeed * Time.deltaTime;

        if (waves[waveNumber].spawnTimer <= 0)
        {
            waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
            SpawnObject();
        }
        if (waves[waveNumber].spawnObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnObjectCount = 0;

            // --- NEW: Reset coinSpawnCount and set new random starting positions for the next wave ---
            waves[waveNumber].coinSpawnCount = 0;
            waves[waveNumber].waveStartJitterY = Random.Range(-1.0f, 1.0f); // Randomize the vertical center
            waves[waveNumber].waveStartJitterX = Random.Range(0f, 0.4f);   // Randomize the horizontal stagger

            waveNumber++;
            if (waveNumber >= waves.Count)
            {
                waveNumber = 0;
            }
        }
    }

    private void SpawnObject()
    {
        GameObject spawnedObject = waves[waveNumber].pool.GetPooledObject();

        // --- EDITED: Choose between random spawn or coin path spawn ---
        if (waves[waveNumber].isCoinWave)
        {
            spawnedObject.transform.position = CoinPath();
        }
        else
        {
            spawnedObject.transform.position = RandomSpawnPoint();
        }
        // spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNumber].spawnObjectCount++;
    }

    // Existing random spawn logic for non-coin objects
    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minPos.position.x;
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }

    /// <summary>
    /// Spawns objects in a line, with a slight vertical offset, for perfect wave patterns.
    /// EDITED: Now uses fixed per-wave randomized jitter for a more organic feel.
    /// </summary>
    private Vector2 CoinPath()
    {
        // Get the current wave reference
        Wave currentWave = waves[waveNumber];

        // 1. Calculate a fixed vertical position for the whole coin wave (centerY)
        // This ensures the wave oscillates around a central point, like Jetpack Joyride.
        float waveCenterY = Mathf.Lerp(minPos.position.y, maxPos.position.y, 0.5f);

        // 2. The single, randomized vertical offset for the entire wave, making it feel organic.
        float jitterY = currentWave.waveStartJitterY;

        // 3. Add a slight vertical progression based on the coin count (the core line for the wave to follow)
        // This creates a shallow, gentle slope for the wave to ride on.
        

        Vector2 spawnPoint;

        // X position is fixed at the spawn edge
        // Use the fixed, randomized stagger for the entire wave sequence (waveStartJitterX).
        float jitterX = currentWave.waveStartJitterX;
        spawnPoint.x = minPos.position.x + jitterX;

        // Y position is determined by the center, plus the fixed wave jitter and progression.
        spawnPoint.y = waveCenterY + jitterY;

        // Increment the coin count for the next spawn
        currentWave.coinSpawnCount++;

        return spawnPoint;
    }
}

