using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalBullet : Bullet
{
    [SerializeField] ParticleSystem onHitParticle = null;

    protected override void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) { return; }

        base.OnTriggerEnter(other);
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Enemy>() != null)
            {
                DealDamageToEnemy(other.GetComponent<Enemy>());
            }

        }
        StartCoroutine(HitVFX());

    }

    protected virtual void DealDamageToEnemy(Enemy enemy)
    {
        enemy.GetHit(BulletPower);
    }

    IEnumerator HitVFX()
    {
        if (onHitParticle != null)
        {
            onHitParticle.Play();
        }
        yield return new WaitForSeconds(onHitParticle.main.duration);

        gameObject.SetActive(false);

    }
}
