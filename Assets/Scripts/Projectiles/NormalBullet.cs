using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : FlyForwardTagetBullet
{
    [SerializeField] ParticleSystem projectileParticle = null;
    [SerializeField] ParticleSystem onHitParticle = null;
    

    protected override void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) { return; }
        StartCoroutine(HitExplosion());
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit(BulletPower);
        }
        base.OnTriggerEnter(other);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        projectileParticle.gameObject.SetActive(true);
    }

    IEnumerator HitExplosion()
    {
        projectileParticle.gameObject.SetActive(false);
        onHitParticle.Play();
        yield return new WaitForSeconds(onHitParticle.main.duration);
        gameObject.SetActive(false);
    }
}
