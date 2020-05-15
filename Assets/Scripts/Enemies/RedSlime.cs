using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSlime : Enemy
{
    [SerializeField] AnimationClip dieAnimationClip = null;
    [SerializeField] AnimationClip reachedGoalAnimationClip = null;
    Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void ReachGoal()
    {
        if (isDead || reachedGoal) { return; }
        StartCoroutine(GoalReaching());
    }
    public override void Die()
    {
        if (isDead || reachedGoal) { return; }
        StartCoroutine(Dying());
    }

    IEnumerator GoalReaching()
    {
        base.OnReachingGoal();
        animator.SetTrigger("Win");
        yield return new WaitForSeconds(reachedGoalAnimationClip.length);
        Destroy(gameObject);
        base.OnReachedGoal();
    }
    IEnumerator Dying()
    {
        base.OnDying();

        animator.SetTrigger("Die");
        yield return new WaitForSeconds(dieAnimationClip.length + 0.1f);
        Destroy(gameObject);

        base.OnDied();
    }
}
