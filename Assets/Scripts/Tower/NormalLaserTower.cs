using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buffs;
public class NormalLaserTower : LaserTower
{
    [SerializeField] LineRenderer firstLaserBeam = null;
    [SerializeField] LineRenderer secondLaserBeam = null;
    [SerializeField] LineRenderer thirdLaserBeam = null;

    [Header("Level 0")]
    [SerializeField] float level0EffectRange = 1f;
    [SerializeField] float level0Power = 1f;
    [Space]
    [SerializeField] GameObject level0TowerModel = null;
    [SerializeField] Transform level0ShootingPoint = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level0Collider = null;

    [Header("Level 1")]
    [SerializeField] float level1EffectRange = 1f;
    [SerializeField] float level1Power = 1f;
    [Space]
    [SerializeField] GameObject level1TowerModel = null;
    [SerializeField] Transform level1ShootingPoint = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level1Collider = null;

    [Header("Level 2")]
    [SerializeField] float level2EffectRange = 1f;
    [SerializeField] float level2Power = 1f;
    [Space]
    [SerializeField] GameObject level2TowerModel = null;
    [SerializeField] Transform level2ShootingPoint = null;
    [SerializeField] [Tooltip("For receiving mouse input")] BoxCollider level2Collider = null;


    Enemy firstTarget;
    Enemy secondTarget;
    Enemy thirdTarget;

    protected override void Start()
    {
        base.Start();
        firstLaserBeam.useWorldSpace = true;
        secondLaserBeam.useWorldSpace = true;
        thirdLaserBeam.useWorldSpace = true;
    }

    private void Update()
    {
        SeekTarget();
        Shoot();
    }

    public override void Shoot()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    if (firstTarget != null)
                    {
                        ShootLaser(firstLaserBeam, firstTarget);
                    }
                    break;
                }
            case 1:
                {
                    if (firstTarget != null)
                    {
                        ShootLaser(firstLaserBeam, firstTarget);
                    }
                    if (secondTarget != null)
                    {
                        ShootLaser(secondLaserBeam, secondTarget);
                    }
                    break;
                }
            case 2:
            default:
                {
                    if (firstTarget != null)
                    {
                        ShootLaser(firstLaserBeam, firstTarget);
                    }
                    if (secondTarget != null)
                    {
                        ShootLaser(secondLaserBeam, secondTarget);
                    }
                    if (thirdTarget != null)
                    {
                        ShootLaser(thirdLaserBeam, thirdTarget);
                    }
                    break;
                }
        }
    }



    protected override void SeekTarget()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        List<Enemy> detectedEnemies = new List<Enemy>();
        foreach (var hit in hitColliders)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (!enemy.isDead && !enemy.reachedGoal && enemy.isHitable)
                {
                    detectedEnemies.Add(enemy);
                }
            }
        }
        if (firstTarget != null)
        {
            if (detectedEnemies.Contains(firstTarget))
            {
                if (firstTarget.isDead || firstTarget.reachedGoal || !firstTarget.isHitable)
                {
                    firstTarget = null;
                }
            }
            else
            {
                firstTarget = null;
            }
        }
        if (firstTarget == null)
        {
            if (detectedEnemies.Count > 0)
            {
                foreach (var enemy in detectedEnemies)
                {
                    if (enemy == thirdTarget || enemy == secondTarget) { continue; }
                    firstTarget = enemy;
                    break;
                }
            }
        }
        if (CurrentTowerUpgradeLevel >= 1)
        {
            if (secondTarget != null)
            {
                if (detectedEnemies.Contains(secondTarget))
                {
                    if (secondTarget.isDead || secondTarget.reachedGoal || !secondTarget.isHitable)
                    {
                        secondTarget = null;
                    }
                }
                else
                {
                    secondTarget = null;
                }
            }
            if(secondTarget == null)
            {
                if(detectedEnemies.Count > 1)
                {
                    foreach (var enemy in detectedEnemies)
                    {
                        if (enemy == thirdTarget || enemy == firstTarget) { continue; }
                        secondTarget = enemy;
                        break;
                    }
                }
            }
        }
        if(CurrentTowerUpgradeLevel >=2)
        {
            if (thirdTarget != null)
            {
                if (detectedEnemies.Contains(thirdTarget))
                {
                    if (thirdTarget.isDead || thirdTarget.reachedGoal || !thirdTarget.isHitable)
                    {
                        thirdTarget = null;
                    }
                }
                else
                {
                    thirdTarget = null;
                }
            }
            if (thirdTarget == null)
            {
                if (detectedEnemies.Count > 2)
                {
                    foreach(var enemy in detectedEnemies)
                    {
                        if(enemy == firstTarget || enemy == secondTarget) { continue; }
                        thirdTarget = enemy;
                        break;
                    }
                }
            }
        }
        if (firstTarget == null)
        {
            if (firstLaserBeam.enabled)
            {
                firstLaserBeam.enabled = false;
            }
        }
        if (secondTarget == null)
        {
            if (secondLaserBeam.enabled)
            {
                secondLaserBeam.enabled = false;
            }
        }
        if (thirdTarget == null)
        {
            if (thirdLaserBeam.enabled)
            {
                thirdLaserBeam.enabled = false;
            }
        }

        //if (hitColliders.Length == 0)
        //{
        //    firstTarget = null;
        //    secondTarget = null;
        //    thirdTarget = null;
        //    if (firstLaserBeam.enabled)
        //    {
        //        firstLaserBeam.enabled = false;
        //    }
        //    if (secondLaserBeam.enabled)
        //    {
        //        secondLaserBeam.enabled = false;
        //    }
        //    if (thirdLaserBeam.enabled)
        //    {
        //        thirdLaserBeam.enabled = false;
        //    }
        //    return;
        //}
        //if (hitColliders.Length == 1)
        //{
        //    firstTarget = null;
        //    secondTarget = null;
        //    thirdTarget = null;
        //    if (secondLaserBeam.enabled)
        //    {
        //        secondLaserBeam.enabled = false;
        //    }
        //    if (thirdLaserBeam.enabled)
        //    {
        //        thirdLaserBeam.enabled = false;
        //    }
        //    Enemy first = hitColliders[0].GetComponent<Enemy>();
        //    if (first != null)
        //    {
        //        if (!first.isDead && !first.reachedGoal && first.isHitable)
        //        {
        //            firstTarget = first;
        //        }
        //    }
        //    else
        //    {
        //        if (firstLaserBeam.enabled)
        //        {
        //            firstLaserBeam.enabled = false;
        //        }
        //    }
        //}
        //else
        //{

        //    if (detectedEnemies.Count > 0)
        //    {
        //        if (firstTarget != null)
        //        {
        //            if (detectedEnemies.Contains(firstTarget))
        //            {
        //                if (!firstTarget.isHitable || firstTarget.isDead || firstTarget.reachedGoal)
        //                {
        //                    firstTarget = null;
        //                }
        //            }
        //            else
        //            {
        //                firstTarget = null;
        //            }
        //        }
        //        if (secondTarget != null)
        //        {
        //            if (detectedEnemies.Contains(secondTarget))
        //            {
        //                if (!secondTarget.isHitable || secondTarget.isDead || secondTarget.reachedGoal)
        //                {
        //                    secondTarget = null;
        //                }
        //            }
        //            else
        //            {
        //                secondTarget = null;
        //            }
        //        }
        //        if (thirdTarget != null)
        //        {
        //            if (detectedEnemies.Contains(thirdTarget))
        //            {
        //                if (!thirdTarget.isHitable || thirdTarget.isDead || thirdTarget.reachedGoal)
        //                {
        //                    thirdTarget = null;
        //                }
        //            }
        //            else
        //            {
        //                thirdTarget = null;
        //            }
        //        }

        //        if (CurrentTowerUpgradeLevel <= 0)
        //        {
        //            if (firstTarget == null)
        //            {
        //                firstTarget = detectedEnemies[0];
        //            }
        //        }
        //        else
        //        {
        //            if (detectedEnemies.Count == 1)
        //            {
        //                if (firstTarget == null)
        //                {
        //                    firstTarget = detectedEnemies[0];
        //                }
        //            }
        //            else if(detectedEnemies.Count == 2)
        //            {
        //                if (firstTarget == null)
        //                {
        //                    firstTarget = detectedEnemies[0];
        //                }
        //                if (secondTarget == null)
        //                {
        //                    firstTarget = detectedEnemies[1];
        //                }
        //            }
        //            else
        //            {
        //                if (firstTarget == null)
        //                {
        //                    firstTarget = detectedEnemies[0];
        //                }
        //                if (secondTarget == null)
        //                {
        //                    secondTarget = detectedEnemies[1];
        //                }
        //                 if (thirdTarget == null)
        //                {
        //                    thirdTarget = detectedEnemies[2];
        //                }
        //            }

        //        }
        //    }
        //}
    }

    protected override void SetUpTower()
    {
        switch (CurrentTowerUpgradeLevel)
        {
            case 0:
                {
                    EffectRangeRadius.BaseValue = level0EffectRange;
                    DPS.BaseValue = level0Power;

                    level0TowerModel.SetActive(true);
                    level1TowerModel.SetActive(false);
                    level2TowerModel.SetActive(false);

                    SetUpCollider(level0Collider);
                    currentShootingPoint = level0ShootingPoint;
                    break;
                }
            case 1:
                {
                    EffectRangeRadius.BaseValue = level1EffectRange;
                    DPS.BaseValue = level1Power;

                    level0TowerModel.SetActive(false);
                    level1TowerModel.SetActive(true);
                    level2TowerModel.SetActive(false);

                    SetUpCollider(level1Collider);
                    currentShootingPoint = level1ShootingPoint;
                    break;
                }
            case 2:
            default:
                {
                    EffectRangeRadius.BaseValue = level2EffectRange;
                    DPS.BaseValue = level2Power;

                    level0TowerModel.SetActive(false);
                    level1TowerModel.SetActive(false);
                    level2TowerModel.SetActive(true);

                    SetUpCollider(level2Collider);
                    currentShootingPoint = level2ShootingPoint;
                    break;
                }
        }
    }

    private void ShootLaser(LineRenderer laserBeam, Enemy target)
    {
        if (laserBeam == null || target == null) { return; }
        if (!laserBeam.enabled)
        {
            laserBeam.enabled = true;
        }
        laserBeam.SetPosition(0, currentShootingPoint.position);
        if (target.Body != null)
        {
            laserBeam.SetPosition(1, target.Body.position);
        }
        else
        {
            laserBeam.SetPosition(1, target.transform.position);
        }
        DealDamagePerSecond(target);
    }
}
