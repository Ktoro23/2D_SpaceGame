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
        public GameObject preFabs;
        public float spawnTimer;
        public float spawnInterval;
        public int objectsPerWave;
        public int spawnObjectCount;
    }
    // Update is called once per frame
    void Update()
    {
        waves[waveNumber].spawnTimer += Time.deltaTime * PlayerMovement.Instance.boost;
        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
               waves[waveNumber].spawnTimer = 0;
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
        Instantiate(waves[waveNumber].preFabs, RandomSpawnPoint(), transform.rotation, transform);
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
