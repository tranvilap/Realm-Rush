using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerBuffs;
public class PoisonLaserTower : LaserTower
{
    [SerializeField] LineRenderer firstLaserBeam;
    [SerializeField] LineRenderer secondLaserBeam;
    [SerializeField] LineRenderer thirdLaserBeam;

    [SerializeField] BaseTowerBuff poisonLaserDebuff;

    Enemy firstTarget;
    Enemy secondTarget;
    Enemy thirdTarget;

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
                    if(firstTarget != null)
                    {
                        ShootLaser(firstLaserBeam, firstTarget);
                    }
                    if(secondTarget != null)
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
                    if(thirdTarget != null)
                    {
                        ShootLaser(thirdLaserBeam, thirdTarget);
                    }
                    break;
                }
        }

        ShootLaser(firstLaserBeam, firstTarget);
        ShootLaser(secondLaserBeam, secondTarget);
        ShootLaser(thirdLaserBeam, thirdTarget);
    }

    protected override void SeekTarget()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, EffectRangeRadius.CalculatedValue, WhatIsTarget);
        if (hitColliders.Length == 0)
        {
            firstTarget = null;
            secondTarget = null;
            thirdTarget = null;
            if (firstLaserBeam.enabled)
            {
                firstLaserBeam.enabled = false;
            }
            if (secondLaserBeam.enabled)
            {
                secondLaserBeam.enabled = false;
            }
            if (thirdLaserBeam.enabled)
            {
                thirdLaserBeam.enabled = false;
            }
            return;
        }
        if (hitColliders.Length == 1)
        {
            firstTarget = null;
            secondTarget = null;
            thirdTarget = null;
            if (secondLaserBeam.enabled)
            {
                secondLaserBeam.enabled = false;
            }
            if (thirdLaserBeam.enabled)
            {
                thirdLaserBeam.enabled = false;
            }
            Enemy first = hitColliders[0].GetComponent<Enemy>();
            if (first != null)
            {
                firstTarget = first;
            }
            else
            {
                if (firstLaserBeam.enabled)
                {
                    firstLaserBeam.enabled = false;
                }
            }
        }
        else
        {
            firstTarget = null;
            secondTarget = null;
            thirdTarget = null;
            foreach (var hit in hitColliders)
            {
                var enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    if (CurrentTowerUpgradeLevel <= 0)
                    {
                        if (firstTarget == null)
                        {
                            firstTarget = enemy;
                            break;
                        }
                    }
                    else
                    {
                        if (firstTarget == null)
                        {
                            firstTarget = enemy;
                        }
                        else if (secondTarget == null)
                        {
                            secondTarget = enemy;
                        }
                        else if (thirdTarget == null)
                        {
                            thirdTarget = enemy;
                        }
                        else
                        {
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
            if (thirdLaserBeam == null)
            {
                if (thirdLaserBeam.enabled)
                {
                    thirdLaserBeam.enabled = false;
                }
            }
        }
    }

    protected override void SetUpTower()
    {

    }

    private void ShootLaser(LineRenderer laserBeam, Enemy target)
    {
        if (laserBeam == null || target == null) { return; }
        if (!laserBeam.enabled)
        {
            laserBeam.enabled = true;
        }
        laserBeam.SetPosition(0, shootingPoint.position);
        laserBeam.SetPosition(1, target.transform.position);
    }
}
