using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] GameObject prefabToPool = null;
    [SerializeField] [Min(0f)] int amountToPool = 3;
    List<GameObject> pooledAmmo = new List<GameObject>();

    public GameObject GetObject()
    {
        for (int i = 0; i < pooledAmmo.Count; i++)
        {
            if (!pooledAmmo[i].gameObject.activeInHierarchy)
            {
                return pooledAmmo[i];
            }
        }
        return null;
    }

    void Awake()
    {
        if (prefabToPool != null)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefabToPool, this.transform);
                obj.gameObject.SetActive(false);
                pooledAmmo.Add(obj);
            }
        }
    }

}
