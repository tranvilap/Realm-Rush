using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletPooler))]
public abstract class ShootingTower : Tower, IShootable
{
    [SerializeField] private float firingRate = 0.3f;
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private float power = 1f;
    [SerializeField] protected Transform shootingPoint = null;
    public float Power { get => power; set => power = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float FiringRate { get => firingRate; set => firingRate = value; }

    protected  Bullet PrepareBullet()
    {
        var bullet = bulletPooler.GetBullet();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public abstract void Shoot();
}
