using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefabToPool = null;
    [SerializeField] [Min(0f)] int amountToPool = 3;
    List<GameObject> pooledAmmo = new List<GameObject>();

    public GameObject GetBullet()
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

    void Start()
    {
        if (ammoPrefabToPool != null)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(ammoPrefabToPool, this.transform);
                obj.gameObject.SetActive(false);
                pooledAmmo.Add(obj);
            }
        }
    }

}
