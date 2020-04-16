using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    [SerializeField] protected ParticleSystem onHitParticle = null;
    bool reachedGoal = false;
    Collider collider;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        CheckIfReachGoal(pos);
    }

    public override void Die()
    {
        base.Die();
        collider.enabled = false;
        isHitable = false;
        isDead = true;
        if (dieParticle != null)
        {
            dieParticle.Play();
            Destroy(gameObject, dieParticle.main.duration);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void GetHit(float damage)
    {
        if (!isHitable) { return; }
        if (isDead) { return; }
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Die();
        }
        else
        {
            if (onHitParticle != null)
            {
                onHitParticle.Play();
            }
        }
    }

    public override void ReachGoal()
    {
        if(isDead) { return; }
        base.ReachGoal();
        isHitable = false;
        reachedGoal = true;
        float destroyDelay = 0.5f;
        if (reachedGoalParticle != null)
        {
            destroyDelay = reachedGoalParticle.main.duration;
            reachedGoalParticle.Play();
        }
        Destroy(gameObject, destroyDelay);
    }

    private void CheckIfReachGoal(Vector3 pos)
    {
        if (pos.x == endWaypoint.GridPos.x && pos.z == endWaypoint.GridPos.y && !isDead)
        {
            if (!reachedGoal)
            {
                ReachGoal();
            }
        }
    }


}
