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

    private float firingTimer = 0f;
    
    protected Bullet PrepareBullet()
    {
        var bullet = bulletPooler.GetObject();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
        return bullet.GetComponent<Bullet>();
    }
    public virtual void Shoot(Transform target)
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
    }

}
