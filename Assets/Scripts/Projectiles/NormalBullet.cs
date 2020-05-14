using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalBullet : Bullet
{
    [SerializeField] ParticleSystem onHitEnemyParticle = null;
    [SerializeField] ParticleSystem onHitGroundParticle = null;
    //[SerializeField] AudioClip onHitGroundSFX = null;
    //[SerializeField] AudioClip onHitEnemySFX = null;

    AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
            StartCoroutine(HitEffect(onHitGroundParticle));
        }
    }

    protected virtual void DealDamageToEnemy(Enemy enemy)
    {
        enemy.GetHit(BulletPower);
        StartCoroutine(HitEffect(onHitEnemyParticle));
    }

    IEnumerator HitEffect(ParticleSystem particle)
    {
        if (particle != null)
        {
            particle.Play();
        }
        
        //if(SFX != null)
        //{
        //    if (audioSource != null)
        //    {
        //        audioSource.PlayOneShot(SFX);
        //    }
        //}
        yield return new WaitForSeconds(particle.main.duration);

        gameObject.SetActive(false);

    }
}
