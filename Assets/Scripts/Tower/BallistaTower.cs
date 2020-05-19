using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;
public class BallistaTower : ShootingTower
{
    [Header("Level 0")]
    [SerializeField] float level0EffectRange = 1f;
    [SerializeField] float level0FiringRate = 1f;
    [SerializeField] float level0BulletSpeed = 1f;
    [SerializeField] float level0Power = 1f;
    [Space]
    [SerializeField] GameObject level0TowerModel = null;
    [SerializeField] Transform level0ShootingPoint = null;
    [SerializeField] Transform level0Balista = null;
    [SerializeField] Transform level0Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level0Collider = null;

    [Header("Level 1")]
    [SerializeField] float level1EffectRange = 1f;
    [SerializeField] float level1FiringRate = 1f;
    [SerializeField] float level1BulletSpeed = 1f;
    [SerializeField] float level1Power = 1f;
    [Space]
    [SerializeField] GameObject level1TowerModel = null;
    [SerializeField] Transform level1ShootingPoint = null;
    [SerializeField] Transform level1Balista = null;
    [SerializeField] Transform level1Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level1Collider = null;

    [Header("Level 2")]
    [SerializeField] float level2EffectRange = 1f;
    [SerializeField] float level2FiringRate = 1f;
    [SerializeField] float level2BulletSpeed = 1f;
    [SerializeField] float level2Power = 1f;
    [Space]
    [SerializeField] GameObject level2TowerModel = null;
    [SerializeField] Transform level2ShootingPoint = null;
    [SerializeField] Transform level2Balista = null;
    [SerializeField] Transform level2Bow = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level2Collider = null;

    [Header("Level 2 - Second Balista")]
    [SerializeField] [Range(0f, 1f)] [Tooltip("Excluded Firing rate and effect range")] float percentOfTotalPower = 0.5f;
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
                            bullet.AimTo(shootingPoint, target, BulletSpeed.CalculatedValue, Power.CalculatedValue);
                            bullet.gameObject.SetActive(true);
                            bullet.Shoot();
                            PlayOnShootSFX();
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
                    if (bullet != null)
                    {
                        bullet.AimTo(shootingPoint, target, BulletSpeed.CalculatedValue, Power.CalculatedValue);
                        bullet.gameObject.SetActive(true);
                        bullet.Shoot();
                        PlayOnShootSFX();
                    }
                    var secondBullet = PrepareBulletAt(secondShootingPoint);
                    if (secondBullet != null)
                    {
                        secondBullet.AimTo(secondShootingPoint, secondTargetEnemy,
                            BulletSpeed.CalculatedValue * percentOfTotalPower, Power.CalculatedValue * percentOfTotalPower);
                        secondBullet.gameObject.SetActive(true);
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

    private void PlayOnShootSFX()
    {
        AudioManager.PlayOneShotSound(audioSource, onShootSFX);
    }

    protected override void SeekTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        if (hitColliders.Length == 0) { currentTargetEnemy = null; return; }
        currentTargetEnemy = hitColliders[0].transform;
        Vector3 balistaTarget = new Vector3(currentTargetEnemy.transform.position.x,
                                        balistaToPan.transform.position.y,
                                        currentTargetEnemy.transform.position.z);

        balistaToPan.LookAt(balistaTarget);
        bowToPan.LookAt(currentTargetEnemy);

        if (CurrentTowerUpgradeLevel >= 2)
        {
            if (hitColliders.Length < 2)
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

    protected override void SetUpTower()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    EffectRangeRadius.BaseValue = level0EffectRange;
                    FiringRate.BaseValue = level0FiringRate;
                    BulletSpeed.BaseValue = level0BulletSpeed;
                    Power.BaseValue = level0Power;

                    level0TowerModel.SetActive(true);
                    level1TowerModel.SetActive(false);
                    level2TowerModel.SetActive(false);

                    SetUpCollider(level0Collider);
                    shootingPoint = level0ShootingPoint;
                    balistaToPan = level0Balista;
                    bowToPan = level0Bow;
                    break;
                }
            case 1:
                {
                    EffectRangeRadius.BaseValue = level1EffectRange;
                    FiringRate.BaseValue = level1FiringRate;
                    BulletSpeed.BaseValue = level1BulletSpeed;
                    Power.BaseValue = level1Power;

                    level0TowerModel.SetActive(false);
                    level1TowerModel.SetActive(true);
                    level2TowerModel.SetActive(false);

                    SetUpCollider(level1Collider);
                    shootingPoint = level1ShootingPoint;
                    balistaToPan = level1Balista;
                    bowToPan = level1Bow;
                    break;
                }
            case 2:
            default:
                {
                    EffectRangeRadius.BaseValue = level2EffectRange;
                    FiringRate.BaseValue = level2FiringRate;
                    BulletSpeed.BaseValue = level2BulletSpeed;
                    Power.BaseValue = level2Power;

                    level0TowerModel.SetActive(false);
                    level1TowerModel.SetActive(false);
                    level2TowerModel.SetActive(true);

                    SetUpCollider(level2Collider);
                    shootingPoint = level2ShootingPoint;
                    balistaToPan = level2Balista;
                    bowToPan = level2Bow;
                    break;
                }
        }
    }
}
