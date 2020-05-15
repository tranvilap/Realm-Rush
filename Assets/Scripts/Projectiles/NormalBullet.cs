using Game.Sound;
using System.Collections;
using UnityEngine;
using DigitalRuby.SoundManagerNamespace;
public abstract class NormalBullet : Bullet
{
    [SerializeField] ParticleSystem onHitEnemyParticle = null;
    [SerializeField] ParticleSystem onHitGroundParticle = null;

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
                CollideWithEnemy(other.GetComponent<Enemy>(), other.transform);
            }
            else
            {
                Debug.LogWarning("Has tag Enemy but doesn't has Enemy script");
            }
        }
        else
        {
            CollideWithOther(other.transform);
        }
    }

    protected virtual void CollideWithOther(Transform atTransform)
    {
        StartCoroutine(CollideEffect(onHitGroundParticle));
    }

    protected virtual void CollideWithEnemy(Enemy enemy, Transform atTransform)
    {
        DealDamageTo(enemy);
        StartCoroutine(CollideEffect(onHitEnemyParticle));
    }

    private void DealDamageTo(Enemy enemy)
    {
        enemy.GetHit(BulletPower);
    }

    IEnumerator CollideEffect(ParticleSystem particle)
    {
        if (particle != null)
        {
            yield return new WaitForSeconds(particle.main.duration);
        }
        gameObject.SetActive(false);
    }
}
