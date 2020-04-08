using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonTower : ShootingTower
{
    [SerializeField] Transform cannonTopToPan = null;
    [SerializeField] Transform shootingPoint = null;

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

    private Bullet PrepareBullet()
    {
        var bullet = ammoPooler.GetBullet();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
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
