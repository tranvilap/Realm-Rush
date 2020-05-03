using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaTower : ShootingTower
{
    [SerializeField] Transform balistaToPan = null;
    [SerializeField] Transform bowToPan = null;

    private float firingTimer = 0f;
    private Transform secondTargetEnemy = null;

    void Update()
    {
        if (balistaToPan == null || bowToPan == null) { return; }
        SeekTarget();
        if (currentTargetEnemy == null) { return; }
        Shoot(currentTargetEnemy);

    }

    public override void Shoot(Transform target)
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
            case 1:
            case 2:
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
                    break;
                }
            case 3:
                {
                    break;
                }
            default:
                break;
        }
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRadius, WhatIsTarger);
        if (hitColliders.Length == 0) { currentTargetEnemy = null; return; }
        if (CurrentTowerUpgradeLevel == 3)
        {

        }
        else
        {
            currentTargetEnemy = hitColliders[0].transform;
            Vector3 balistaTarget = new Vector3(currentTargetEnemy.transform.position.x, 
                                            balistaToPan.transform.position.y, 
                                            currentTargetEnemy.transform.position.z);

            balistaToPan.LookAt(balistaTarget);
            bowToPan.LookAt(currentTargetEnemy);
        }

    }
}
