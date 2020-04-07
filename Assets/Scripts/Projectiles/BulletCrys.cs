using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCrys : Bullet
{
    TowerGun towerGun = null;
    // Start is called before the first frame update
    void Start()
    {
        towerGun = GetComponentInParent<TowerGun>();
        if (towerGun != null)
        {
            ChangePower(towerGun.Power);
        }
    }

    public void ChangePower(float amount)
    {
        BulletPower = amount;
    }

}
