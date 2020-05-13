﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaTower : ShootingTower
{

    [Header("Level 0")]
    [SerializeField] GameObject level0Tower = null;
    [SerializeField] Transform level0ShootingPoint = null;
    [SerializeField] Transform level0Balista = null;
    [SerializeField] Transform level0Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level0Collider = null;

    [Header("Level 1")]
    [SerializeField] GameObject level1Tower = null;
    [SerializeField] Transform level1ShootingPoint = null;
    [SerializeField] Transform level1Balista = null;
    [SerializeField] Transform level1Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level1Collider = null;

    [Header("Level 2")]
    [SerializeField] GameObject level2Tower = null;
    [SerializeField] Transform level2ShootingPoint = null;
    [SerializeField] Transform level2Balista = null;
    [SerializeField] Transform level2Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level2Collider = null;

    [Header("Level 2 - Second Balista")]
    [SerializeField] Transform secondShootingPoint = null;
    [SerializeField] Transform secondBalista = null;
    [SerializeField] Transform secondBow = null;

    Transform balistaToPan = null;
    Transform bowToPan = null;
    BoxCollider boxCollider;

    private float firingTimer = 0f;
    private Transform secondTargetEnemy = null;

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider>();
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
                {
                    if (target == null) { return; }
                    if (firingTimer >= FiringRate.CalculatedValue)
                    {
                        var bullet = PrepareBullet();
                        if (bullet != null)
                        {
                            bullet.AimTo(target, BulletSpeed.CalculatedValue, Power.CalculatedValue);
                            bullet.Shoot();
                        }
                        firingTimer = 0f;
                    }
                    else
                    {
                        firingTimer += Time.deltaTime;
                    }
                    break;
                }
            case 2:
            default:
                if (target == null) { return; }
                if (firingTimer >= FiringRate.CalculatedValue)
                {
                    var bullet = PrepareBullet();
                    var secondBullet = PrepareBulletAt(secondShootingPoint);
                    if (bullet != null)
                    {
                        bullet.AimTo(target, BulletSpeed.CalculatedValue, Power.CalculatedValue);
                        bullet.Shoot();
                    }
                    if(secondBullet != null)
                    {
                        secondBullet.AimTo(secondTargetEnemy, BulletSpeed.CalculatedValue, Power.CalculatedValue);
                        secondBullet.Shoot();
                    }
                    firingTimer = 0f;
                }
                else
                {
                    firingTimer += Time.deltaTime;
                }
                break;
        }
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRange.CalculatedValue, WhatIsTarget);
        if (hitColliders.Length == 0) { currentTargetEnemy = null; return; }
        currentTargetEnemy = hitColliders[0].transform;
        Vector3 balistaTarget = new Vector3(currentTargetEnemy.transform.position.x,
                                        balistaToPan.transform.position.y,
                                        currentTargetEnemy.transform.position.z);

        balistaToPan.LookAt(balistaTarget);
        bowToPan.LookAt(currentTargetEnemy);

        if (CurrentTowerUpgradeLevel >= 2)
        {
            if(hitColliders.Length < 2)
            {
                secondTargetEnemy = currentTargetEnemy;
            }
            else
            {
                secondTargetEnemy = hitColliders[1].transform;
            }
            Vector3 secondBalistaTarget = new Vector3(secondTargetEnemy.position.x,
                                        secondBalista.position.y,
                                        secondTargetEnemy.position.z);

            secondBalista.LookAt(secondBalistaTarget);
            secondBow.LookAt(secondTargetEnemy);
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

                    SetUpCollider(level0Collider);
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

                    SetUpCollider(level1Collider);
                    shootingPoint = level1ShootingPoint;
                    balistaToPan = level1Balista;
                    bowToPan = level1Bow;
                    break;
                }
            case 2:
            default:
                {
                    level0Tower.SetActive(false);
                    level1Tower.SetActive(false);
                    level2Tower.SetActive(true);

                    SetUpCollider(level2Collider);
                    shootingPoint = level2ShootingPoint;
                    balistaToPan = level2Balista;
                    bowToPan = level2Bow;
                    break;
                }
        }
    }
    private void SetUpCollider(BoxCollider targetBoxCollider)
    {
        boxCollider.size = targetBoxCollider.size;
        boxCollider.center = targetBoxCollider.center;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        if (!CheckUpgradeable()) { return; }
        SetUpTower();
        GameObject upgradeEffect =  SharedObjectPooler.main.GetPooledObject(Constants.UPGRADE_TOWER_VFX);
        if(upgradeEffect != null)
        {
            Vector3 effectPosition = transform.position;
            effectPosition.y = upgradeEffect.transform.position.y;
            upgradeEffect.transform.position = effectPosition;
            upgradeEffect.SetActive(true);
        }
    }

}
