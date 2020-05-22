using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedObjectPooler : MonoBehaviour
{
    public static SharedObjectPooler main;

    private void Awake()
    {
        main = this;
    }

    [System.Serializable]
    public class ItemToPool
    {
        public string tag;
        public GameObject gameObjectToPool;
        [Min(0)] public int amountToPool;
        public bool shouldExpand = false;
    }

    public List<ItemToPool> itemsToPool;
    Dictionary<string, List<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();
        foreach (ItemToPool item in itemsToPool)
        {
            if(item.gameObjectToPool != null)
            {
                List<GameObject> objectPool = new List<GameObject>();

                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject go = Instantiate(item.gameObjectToPool, transform);
                    go.SetActive(false);
                    objectPool.Add(go);
                }
                poolDictionary.Add(item.tag, objectPool);
            }
        }
    }


    public GameObject GetPooledObject(string tag)
    {
        if (!poolDictionary.TryGetValue(tag, out List<GameObject> pool))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
            return null;
        }
        else
        {
            foreach(GameObject item in pool)
            {
                if(!item.activeInHierarchy)
                {
                    return item;
                }
            }
            foreach(var item in itemsToPool)
            {
                if(item.tag == tag)
                {
                    if(item.gameObjectToPool!= null)
                    {
                        if (item.shouldExpand)
                        {
                            GameObject go = Instantiate(item.gameObjectToPool, transform);
                            go.SetActive(false);
                            pool.Add(go);
                            return go;
                        }
                    }

                    break;
                }
            }
            return null;
        }

    }

    public void AddPooledObject (string tag, GameObject objectToPool, int amount, bool canExpand)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " already't existed");
            return;
        }
        if(objectToPool == null)
        {
            Debug.LogError("Object to pool is null");
            return;
        }
        if(amount <= 0) { amount = 0; }

        var newPoolItem = new ItemToPool();
        newPoolItem.amountToPool = amount;
        newPoolItem.gameObjectToPool = objectToPool;
        newPoolItem.tag = tag;
        newPoolItem.shouldExpand = canExpand;

        itemsToPool.Add(newPoolItem);

        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(objectToPool, transform);
            go.SetActive(false);
            objectPool.Add(go);
        }
        poolDictionary.Add(tag, objectPool);
    }
}
