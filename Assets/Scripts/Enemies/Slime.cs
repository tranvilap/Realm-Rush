using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] AnimationClip dieAnimationClip = null;
    [SerializeField] AnimationClip reachedGoalAnimationClip = null;

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
        yield return new WaitForSeconds(reachedGoalAnimationClip.length);
        Destroy(gameObject);
        base.OnReachedGoal();
    }
    IEnumerator Dying()
    {
        base.OnDying();
        base.OnDied();
        yield return new WaitForSeconds(dieAnimationClip.length);
        Destroy(gameObject);

    }
}
