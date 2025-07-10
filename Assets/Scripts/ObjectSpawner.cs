using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List <Wave> waves ;



    [System.Serializable]
    public class Wave
    {
        
        public ObjectPooler pool;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnObjectCount;
    }
    // Update is called once per frame
    void Update()
    {
        waves[waveNumber].spawnTimer -= Time.deltaTime * GameManger.Instance.worldSpeed;
        if (waves[waveNumber].spawnTimer <= 0)
        {
               waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
            SpawnObject();
        }
        if (waves[waveNumber].spawnObjectCount >= waves[waveNumber].objectsPerWave)
        {
            waves[waveNumber].spawnObjectCount = 0;
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
        spawnedObject.transform.position = RandomSpawnPoint();
        spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNumber].spawnObjectCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minPos.position.x;
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }
}
