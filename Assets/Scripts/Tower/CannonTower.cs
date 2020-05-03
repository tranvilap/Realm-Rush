﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonTower : ShootingTower
{
    [SerializeField] Transform cannonTopToPan = null;

    private float firingTimer = 0f;
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
        if (target == null) { return; }
        if (firingTimer >= firingRate)
        {
            var bullet = PrepareBullet();
            if (bullet != null)
            {
                bullet.AimTo(target, bulletSpeed, power);
            }
            firingTimer = 0f;
        }
        else
        {
            firingTimer += Time.deltaTime;
        }
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

    public override void Upgrade() //Not Update
    {
        if (!CheckUpgradeable()) { return; }
        base.Upgrade();
        //Todo write upgrade logic
        firingRate -= 0.2f;
        bulletSpeed += 0.2f;
        power += 1f;
    }
}
