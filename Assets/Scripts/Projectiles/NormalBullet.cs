using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    protected bool alreadyHit = false;

    protected override void Update()
    {
        base.Update();
        if (target == null) { return; }
        FlyToTarget();
    }

    void FlyToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) { return; }
        base.OnTriggerEnter(other);
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit(BulletPower);
        }
        alreadyHit = true;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        alreadyHit = false;
    }
}
