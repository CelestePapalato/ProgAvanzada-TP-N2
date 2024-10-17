using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class ObjectPoolData
    {
        public GameObject objectToPool;
        public int quantity;
    }

    public static ObjectPool Instance;

    [SerializeField]
    ObjectPoolData[] objectsToPool;

    Dictionary<string, List<GameObject>> poolData = new Dictionary<string, List<GameObject>>();


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        foreach (ObjectPoolData data in objectsToPool)
        {
            for (int i = 0; i < data.quantity; i++)
            {
                GameObject tmp = Instantiate(data.objectToPool);
                tmp.SetActive(false);

                string tag = data.objectToPool.tag;

                if (!poolData.ContainsKey(tag))
                {
                    poolData.Add(tag, new List<GameObject>());
                }
                poolData[tag].Add(tmp);
            }
        }
    }
    
    public GameObject GetObject(string tag)
    {
        GameObject obj = null;
        if (poolData.ContainsKey(tag))
        {
            foreach(GameObject tmp in poolData[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    obj = tmp;
                    break;
                }
            }
        }
        return obj;
    }
}
