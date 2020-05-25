using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PoolableObject[] m_ObjectsToPool = new PoolableObject[0];

    private static readonly Dictionary<int, Queue<SpaceObject>> m_Pool =
        new Dictionary<int, Queue<SpaceObject>>();

    private const int k_DefaultPoolSize = 10;
    private static Transform s_MyTransform = null;

    private void Awake()
    {
        s_MyTransform = transform;
        InitializePool();
    }

    public static SpaceObject Get(SpaceObject prefab)
    {
        if (prefab == null)
        {
            return null;
        }

        int poolKey = prefab.GetInstanceID();

        if (!m_Pool.ContainsKey(poolKey))
        {
            CreatePool(prefab, k_DefaultPoolSize);
        }

        if (m_Pool[poolKey].Count == 0)
        {
            IncreasePool(prefab, poolKey, k_DefaultPoolSize);
        }

        SpaceObject objectToReturn = m_Pool[poolKey].Dequeue();
        objectToReturn.gameObject.SetActive(true);

        return objectToReturn;
    }

    public void Return(SpaceObject spaceObject)
    {
        if (spaceObject == null)
        {
            return;
        }

        PoolIdentifier poolIdentifier = spaceObject.GetComponent<PoolIdentifier>();

        if (poolIdentifier == null)
        {
            return;
        }

        int poolKey = poolIdentifier.PoolKey;
        m_Pool[poolKey].Enqueue(spaceObject);
    }

    private void InitializePool()
    {
        int length = m_ObjectsToPool.Length;
        for (int i = 0; i < length; i++)
        {
            CreatePool(m_ObjectsToPool[i].Prefab, m_ObjectsToPool[i].PoolSize);
        }
    }

    private static void CreatePool(SpaceObject prefab, int poolSize)
    {
        int poolKey = prefab.GetInstanceID();
        if (!m_Pool.ContainsKey(poolKey))
        {
            Transform parent = new GameObject(prefab.name + "s").transform;
            parent.parent = s_MyTransform;
            m_Pool.Add(poolKey, new Queue<SpaceObject>(poolSize));

            for (int i = 0; i < poolSize; i++)
            {
                SpaceObject spaceObject = CreateObject(prefab, parent, poolKey);
                m_Pool[poolKey].Enqueue(spaceObject);
            }
        }
    }

    private static void IncreasePool(SpaceObject prefab, int poolKey, int poolSize)
    {
        Transform parent = new GameObject(prefab.name + "s_Extra").transform;
        parent.parent = s_MyTransform;
        for (int i = 0; i < poolSize; i++)
        {
            SpaceObject spaceObject = CreateObject(prefab, parent, poolKey);
            m_Pool[poolKey].Enqueue(spaceObject);
        }
    }

    private static SpaceObject CreateObject(SpaceObject prefab, Transform parent, int poolKey)
    {
        SpaceObject spaceObject = Instantiate(prefab, parent);

        spaceObject.gameObject.AddComponent<PoolIdentifier>().PoolKey = poolKey;
        spaceObject.name = $"{prefab.name}_Pool";
        spaceObject.gameObject.SetActive(false);
        return spaceObject;
    }

    [Serializable]
    private class PoolableObject
    {
        public SpaceObject Prefab = null;
        public int PoolSize = 10;
    }
}