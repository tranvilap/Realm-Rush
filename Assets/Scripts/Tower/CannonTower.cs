using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonTower : ShootingTower
{
    [SerializeField] Transform cannonTopToPan = null;
   
    
    // Update is called once per frame
    void Update()
    {
        if (cannonTopToPan == null) { return; }
        SeekTarget();
        if (currentTargetEnemy == null) { return; }
        Shoot(currentTargetEnemy);

    }
    
    public override void Shoot(Transform target)
    {
        base.Shoot(target);
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRadius, WhatIsTarger);
        if (hitColliders.Length > 0)
        {
            currentTargetEnemy = hitColliders[0].transform;
            cannonTopToPan.LookAt(currentTargetEnemy);
        }
        else
        {
            currentTargetEnemy = null;
        }
    }

    public override bool Upgrade() //Not Update
    {
        if (!base.Upgrade()) { return false; }
        //Todo write upgrade logic
        firingRate -= 0.2f;
        bulletSpeed += 0.2f;
        power += 1f;
        return true;
    }
}
