using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaTower : ShootingTower
{
    [Header("Level 0")]
    [SerializeField] GameObject level0Tower = null;
    [SerializeField] Transform level0ShootingPoint = null;
    [SerializeField] Transform level0Balista = null;
    [SerializeField] Transform level0Bow = null;
    [Header("Level 1")]
    [SerializeField] GameObject level1Tower = null;
    [SerializeField] Transform level1ShootingPoint = null;
    [SerializeField] Transform level1Balista = null;
    [SerializeField] Transform level1Bow = null;
    [Header("Level 2")]
    [SerializeField] GameObject level2Tower = null;
    [SerializeField] Transform level2ShootingPoint = null;
    [SerializeField] Transform level2Balista = null;
    [SerializeField] Transform level2Bow = null;

    [Header("Level 2 - Second Balista")]
    [SerializeField] Transform secondShootingPoint = null;
    [SerializeField] Transform secondBalista = null;
    [SerializeField] Transform secondBow = null;

    Transform balistaToPan = null;
    Transform bowToPan = null;

    private float firingTimer = 0f;
    private Transform secondTargetEnemy = null;

    protected override void Start()
    {
        base.Start();
        SetUpTower();
    }

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

    private void SetUpTower()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    level0Tower.SetActive(true);
                    level1Tower.SetActive(false);
                    level2Tower.SetActive(false);

                    shootingPoint = level0ShootingPoint;
                    balistaToPan = level0Balista;
                    bowToPan = level0Bow;
                    break;
                }
            case 1:
                {
                    level0Tower.SetActive(false);
                    level1Tower.SetActive(true);
                    level2Tower.SetActive(false);

                    shootingPoint = level1ShootingPoint;
                    balistaToPan = level1Balista;
                    bowToPan = level1Bow;
                    break;
                }
            case 3:
            default:
                {
                    level0Tower.SetActive(false);
                    level1Tower.SetActive(false);
                    level2Tower.SetActive(true);

                    shootingPoint = level2ShootingPoint;
                    balistaToPan = level2Balista;
                    bowToPan = level2Bow;
                    break;
                }
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        SetUpTower();
    }
}
