using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public abstract class ShootingTower : UpgradeableTower, IShootable
{
    [Header("Shooting Info")]
    [SerializeField] protected float baseFiringRate = 0.3f;
    [SerializeField] protected float baseBulletSpeed = 2f;
    [SerializeField] protected float basePower = 1f;
    [SerializeField] protected Transform shootingPoint = null;

    protected Transform currentTargetEnemy = null;

    [HideInInspector] public float currentFiringRate;
    [HideInInspector] public float currentBulletSpeed;
    [HideInInspector] public float currentPower;

    protected override void Start()
    {
        base.Start();
        currentFiringRate = baseFiringRate;
        currentBulletSpeed = baseBulletSpeed;
        currentPower = basePower;
    }

    protected Bullet PrepareBullet()
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    protected Bullet PrepareBulletAt(Transform target)
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = target.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public abstract void Shoot(Transform target);

}
