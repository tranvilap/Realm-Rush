﻿using UnityEngine;
using Stats;

[RequireComponent(typeof(ObjectPooler))]
public abstract class ShootingTower : UpgradeableTower, IShootable
{
    [Header("Shooting Info")]
    [SerializeField] protected BaseStat firingRate;
    [SerializeField] protected BaseStat bulletSpeed;
    [SerializeField] protected BaseStat power;
    [SerializeField] protected Transform shootingPoint = null;

    protected Transform currentTargetEnemy = null;

    public BaseStat FiringRate { get => firingRate; }
    public BaseStat BulletSpeed { get => bulletSpeed; }
    public BaseStat Power { get => power;}

    protected override void Start()
    {
        base.Start();
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
