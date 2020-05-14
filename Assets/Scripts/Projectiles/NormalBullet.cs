using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalBullet : Bullet
{
    [SerializeField] ParticleSystem onHitEnemyParticle = null;
    [SerializeField] ParticleSystem onHitGroundParticle = null;

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
        else
        {
            StartCoroutine(HitVFX(onHitGroundParticle));
        }
    }

    protected virtual void DealDamageToEnemy(Enemy enemy)
    {
        enemy.GetHit(BulletPower);
        StartCoroutine(HitVFX(onHitEnemyParticle));
    }

    IEnumerator HitVFX(ParticleSystem particle)
    {
        if (particle != null)
        {
            particle.Play();
        }
        yield return new WaitForSeconds(particle.main.duration);

        gameObject.SetActive(false);

    }
}
