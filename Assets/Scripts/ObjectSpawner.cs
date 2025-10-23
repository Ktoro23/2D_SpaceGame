using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;

   
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

      
        public int coinSpawnCount;

        [Header("Coin Wave Settings")]
        [Tooltip("If checked, coins will spawn in a perfect line for wave patterns.")]
        public bool isCoinWave;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (GameManger.Instance == null) return;

        waves[waveNumber].spawnTimer -= GameManger.Instance.baseScrollSpeed * Time.deltaTime;
        if (waves[waveNumber].spawnTimer <= 0)
        {
            waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
            SpawnObject();
        }
        if (waves[waveNumber].spawnObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnObjectCount = 0;
            // --- NEW: Reset coinSpawnCount when a new wave starts ---
            waves[waveNumber].coinSpawnCount = 0;
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
    /// </summary>
    private Vector2 CoinPath()
    {
        // Get the current wave reference
        Wave currentWave = waves[waveNumber];

        // 1. Calculate a fixed vertical position for the whole coin wave (centerY)
        // This ensures the wave oscillates around a central point, like Jetpack Joyride.
        float waveCenterY = Mathf.Lerp(minPos.position.y, maxPos.position.y, 0.5f);

        // 2. Add a slight vertical offset based on the position in the wave (optional, can be 0)
        // This creates a diagonal line before the sine wave takes over.
        float verticalOffset = currentWave.coinSpawnCount * 0.1f; // Adjust 0.1f for spacing

        Vector2 spawnPoint;

        // X position is fixed at the spawn edge
        spawnPoint.x = minPos.position.x;

        // Y position is determined by the center, plus the slight offset
        spawnPoint.y = waveCenterY + verticalOffset;

        // Increment the coin count for the next spawn
        currentWave.coinSpawnCount++;

        return spawnPoint;
    }
}
