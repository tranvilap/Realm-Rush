using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrounding : MonoBehaviour
{
    Tower tower;
    // Start is called before the first frame update
    void Start()
    {
        tower = GetComponentInParent<Tower>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(tower.placingPoint == null)
        {
            if (other.CompareTag("Ground"))
            {
                var spawnPoint = other.GetComponent<TowerPlacePoint>();
                if(spawnPoint != null)
                {
                    tower.placingPoint = spawnPoint;
                    spawnPoint.IsPlaceable = false;
                }
            }
        }
    }
}
