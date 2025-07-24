using UnityEngine;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 5;
    private List<GameObject> pool;
    [SerializeField] PoolTypes poolType;
    void Start()
    {
        Createpool();
    }


    private void Createpool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject();
        }
    }


    private GameObject CreateNewObject()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
    private void OnEnable()
    {
        PoolHelper.Subscribe(poolType, this);
    }
    private void OnDisable()
    {
        PoolHelper.UnSubscribe(poolType);
    }
    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeSelf)
            {
                return obj;
            }
        }
        return CreateNewObject();
    }
    public GameObject GetPooledObject(Vector3 position)
    {
        GameObject obj = GetPooledObject();
        obj.SetActive(true);
        obj.transform.position = position;
        return obj;
    }
    public GameObject GetPooledObject(Vector3 position, Quaternion rotation)
    {
        GameObject obj = GetPooledObject(position);
        
        obj.transform.rotation = rotation;
        return obj;
    }
}

public static class PoolHelper
{
    static Dictionary<PoolTypes, ObjectPooler> pools = new Dictionary<PoolTypes, ObjectPooler>();
    public static void Subscribe(PoolTypes poolType, ObjectPooler pool)
    {
        if (pools.ContainsKey(poolType))
            pools.Add(poolType, pool);
        else
            pools[poolType] = pool;
    }
    public static void UnSubscribe(PoolTypes poolType)
    {
        pools.Remove(poolType);
    }

    public static ObjectPooler GetPool(PoolTypes type)
    {
        return pools[type];
    }
}

public enum PoolTypes
{
    Astroid,
    Boom1,
    Boom2,
    bullet1,
    Critter1,
    Critter1_Burn,
    Critter1_Zapped,
    Boss1,
    Boss1Boom,

}