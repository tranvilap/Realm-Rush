using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonTower : ShootingTower
{
    [SerializeField] Transform cannonTopToPan = null;


    Transform currentTargetEnemy = null;
    float timer = 0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (cannonTopToPan == null) { return; }
        SeekTarget();
        if (currentTargetEnemy == null) { return; }
        Shoot();

    }


    public override void Shoot()
    {
        if (timer >= FiringRate)
        {
            var bullet = PrepareBullet();
            if (bullet != null)
            {
                bullet.AimTo(currentTargetEnemy, BulletSpeed, Power);
            }
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
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
}
