using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public abstract class ShootingTower : UpgradeableTower, IShootable
{
    [Header("Shooting Info")]
    [SerializeField] public float firingRate = 0.3f;
    [SerializeField] public float bulletSpeed = 2f;
    [SerializeField] public float power = 1f;
    [SerializeField] protected Transform shootingPoint = null;

    protected Transform currentTargetEnemy = null;
    
    protected Bullet PrepareBullet()
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public abstract void Shoot(Transform target);

}
