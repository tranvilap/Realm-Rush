using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluey : Enemy
{
    [SerializeField] protected ParticleSystem onHitParticle = null;
    [SerializeField] protected ParticleSystem dieParticle = null;
    [SerializeField] protected ParticleSystem reachedGoalParticle = null;

    public override void GetHit(float damage)
    {
        if(isDead || reachedGoal || !isHitable) { return; }
        base.GetHit(damage);
        if(!isDead)
        {
            onHitParticle.Play();
        }
    }

    public override void Die()
    {
        if (isDead || reachedGoal) { return; }
        StartCoroutine(Dying());
    }

    IEnumerator Dying()
    {
        dieParticle.Play();
        yield return new WaitForSeconds(dieParticle.main.duration);
        base.Die();
        Destroy(gameObject);
    }

    public override void ReachGoal()
    {
        if (isDead || reachedGoal) { return; }
        StartCoroutine(GoalReaching());
    }

    IEnumerator GoalReaching()
    {
        reachedGoalParticle.Play();
        yield return new WaitForSeconds(reachedGoalParticle.main.duration);
        base.ReachGoal();
        Destroy(gameObject);
    }
}
