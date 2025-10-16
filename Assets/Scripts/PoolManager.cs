using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public PoolableObject[] poolableObjects;
    private int initialCount = 3;
    [SerializeField] private Dictionary<string, PoolableObject> prefabDict = new Dictionary<string, PoolableObject>();
    [SerializeField] private Dictionary<string, Queue<PoolableObject>> poolQueue = new Dictionary<string, Queue<PoolableObject>>();
    void Awake()
    {
        foreach (PoolableObject poolableObject in poolableObjects)
        {
            prefabDict[poolableObject.name] = poolableObject;
            for (int i = 0; i < initialCount; i++)
            {
                PoolableObject poolableObj = Instantiate(poolableObject, transform.position, Quaternion.identity);
                poolableObj.enabled = false;
                poolableObj.gameObject.SetActive(false);
                poolQueue[poolableObj.name].Enqueue(poolableObject);
            }
        }
    }
    public T Spawn<T>(T obj) where T : PoolableObject
    {
        PoolableObject poolableObject;
        if (!poolQueue.ContainsKey(obj.name)) return null;
        else if (poolQueue[obj.name].Count > 0)
        {
            poolableObject = poolQueue[obj.name].Dequeue();
            poolableObject.enabled = true;
            poolableObject.gameObject.SetActive(true);
        }
        else
        {
            poolableObject = Instantiate(prefabDict[obj.name], transform.position, Quaternion.identity);
        }
        poolableObject.OnSpawn();
        return (T)poolableObject;
    }
    public T Despawn<T>(T obj) where T : PoolableObject
    {
        PoolableObject poolableObject = obj;
        obj.OnDespawn();
        if (!poolQueue.ContainsKey(obj.name)) return null;
        else if (poolQueue[obj.name].Count < initialCount)
        {
            poolQueue[obj.name].Enqueue(obj);
            obj.enabled = false;
            obj.gameObject.SetActive(false);
        }
        else
        {
            Destroy(obj);
        }
        return (T)poolableObject;
    }
}