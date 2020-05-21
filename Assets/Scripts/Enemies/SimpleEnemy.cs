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

    protected float lastMovementSpeed = 0f;

    protected override void Start()
    {
        base.Start();
        lastMovementSpeed = MoveSpeed.CalculatedValue;
        animator = GetComponent<Animator>();
        movingAnimationSpeedMultHashID = Animator.StringToHash(movingAnimationSpeedMultParam);
    }

    protected override void Update()
    {
        base.Update();
        ControlMovingAnimationSpeed();
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

    protected virtual void ControlMovingAnimationSpeed()
    {
        if (animator != null)
        {
            if (MoveSpeed.CalculatedValue >= 0)
            {
                if (lastMovementSpeed != MoveSpeed.CalculatedValue)
                {
                    if (MoveSpeed.BaseValue == 0)
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, MoveSpeed.CalculatedValue);
                    }
                    else
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, MoveSpeed.CalculatedValue / MoveSpeed.BaseValue);
                    }
                    lastMovementSpeed = MoveSpeed.CalculatedValue;
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
