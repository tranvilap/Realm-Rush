using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : FlyForwardTagetBullet
{
    [SerializeField] private TrailRenderer trailRenderer = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        trailRenderer.Clear();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) { return; }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit(BulletPower);
        }
        base.OnTriggerEnter(other);
        gameObject.SetActive(false);
    }
}
