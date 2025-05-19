using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform minPos;
    [SerializeField] private Transform maxPos;

    [SerializeField] private int waveNumber;
    [SerializeField] private List


    public GameObject preFabs;
    public float spawnTimer;
    public float spawnInterval;
   
    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime * PlayerMovement.Instance.boost;
        if(spawnTimer >= spawnInterval)
        {
            spawnTimer = 0;
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Instantiate(preFabs, RandomSpawnPoint(), transform.rotation);
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minPos.position.x;
        spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);

        return spawnPoint;
    }
}
