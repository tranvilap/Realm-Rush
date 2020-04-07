using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonGun : TowerGun
{
    [SerializeField] Transform cannonTopToPan = null;
    [SerializeField] Transform currentTargetEnemy = null;
    [SerializeField] Transform shootingPoint = null;

    float timer = 0f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (cannonTopToPan == null) { return; }
        if (currentTargetEnemy == null) { return; }
        SeekEnemy();
        Shoot();

    }

    private Bullet PrepareBullet()
    {
        var bullet = ammoPooler.GetBullet();
        if (bullet == null) { return null; }
        bullet.transform.position = shootingPoint.position;
        return bullet.GetComponent<Bullet>();
    }

    protected override void Shoot()
    {
        if (timer >= FiringRate)
        {
            var bullet = PrepareBullet();
            if (bullet != null)
            {
                bullet.ShootTo(currentTargetEnemy, BulletSpeed, Power);
            }
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    protected override void SeekEnemy()
    {
        cannonTopToPan.LookAt(currentTargetEnemy);
    }
}
