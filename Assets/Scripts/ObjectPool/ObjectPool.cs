using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int startingAmount = 0;
    
    private List<GameObject> pooledObjects;

    void Awake()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < startingAmount; ++i)
        {
            SpawnAndPoolObject();
        }
    }

    public GameObject GetObject()
    {
        if (pooledObjects.Count == 0)
        {
            SpawnAndPoolObject();
        }

        var obj = pooledObjects[0];
        pooledObjects.Remove(obj);
        
        obj.transform.localScale = Vector3.one;
        obj.SetActive(true);
        
        return obj;
    }

    public void PoolObject(GameObject toPool)
    {
        pooledObjects.Add(toPool);
        toPool.SetActive(false);
        toPool.transform.SetParent(transform);
    }

    private void SpawnAndPoolObject()
    {
        var obj = Instantiate(prefab) as GameObject;
        PoolObject(obj);
    }
}
