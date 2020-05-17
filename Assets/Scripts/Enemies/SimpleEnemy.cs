using Game.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy
{
    [SerializeField] AnimationClip dieAnimationClip = null;
    [SerializeField] AnimationClip reachedGoalAnimationClip = null;

    protected Animator animator;

    protected string movingAnimationSpeedMultParam = "speedMultiplier";
    protected int movingAnimationSpeedMultHashID;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        movingAnimationSpeedMultHashID = Animator.StringToHash(movingAnimationSpeedMultParam);
    }

    protected override void Update()
    {
        base.Update();
        ControlMovingAnimation();
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
        PlayWinAnimation();
        yield return new WaitForSeconds(reachedGoalAnimationClip.length);
        Destroy(gameObject);
        base.OnReachedGoal();
    }
    IEnumerator Dying()
    {
        base.OnDying();
        PlayDieAnimation();
        base.OnDied();
        yield return new WaitForSeconds(dieAnimationClip.length);
        Destroy(gameObject);

    }

    protected virtual void ControlMovingAnimation()
    {
        if (animator != null || enemyMovementScript != null)
        {
            if (enemyMovementScript.movingSpeed.CalculatedValue >= 0)
            {
                if (lastMovementSpeed != enemyMovementScript.movingSpeed.CalculatedValue)
                {
                    if (enemyMovementScript.movingSpeed.BaseValue == 0)
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, enemyMovementScript.movingSpeed.CalculatedValue);
                    }
                    else
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, enemyMovementScript.movingSpeed.CalculatedValue / enemyMovementScript.movingSpeed.BaseValue);
                    }
                    lastMovementSpeed = enemyMovementScript.movingSpeed.CalculatedValue;
                }
            }
        }
    }

    protected virtual void PlayDieAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }
    protected virtual void PlayWinAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Win");
        }
    }
}
