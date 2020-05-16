using Game.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    Animator animator;

    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] private int money;

    [Header("SFX")]
    [SerializeField] SFXObj movingSFX = null;
    [SerializeField] SFXObj damageSFX = null;
    [SerializeField] SFXObj dieSFX = null;
    [SerializeField] SFXObj attackSFX = null;

    protected string movingAnimationSpeedMultParam = "speedMultiplier";
    protected int movingAnimationSpeedMultHashID;

    public event Action OnEnemyDieEvent;

    public bool isHitable = true;
    public bool isDead = false;
    public bool reachedGoal = false;

    protected Waypoint goal;

    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Money { get => money; protected set => money = value; }

    protected Collider hitCollider;
    protected AudioSource audioSource;
    protected EnemyMovement enemyMovementScript = null;

    protected virtual void Start()
    {
        var parent = GameObject.Find("Enemies");
        if (parent == null)
        {
            parent = new GameObject("Enemies");
            parent.transform.position = Vector3.zero;
        }
        transform.parent = parent.transform;

        Map map = FindObjectOfType<Map>();
        if (map != null)
        {
            goal = map.EndWaypoint;
        }

        hitCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        enemyMovementScript = GetComponent<EnemyMovement>();

        movingAnimationSpeedMultHashID = Animator.StringToHash(movingAnimationSpeedMultParam);

        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemySpawned(this));
        }
    }

    protected virtual void Update()
    {
        CheckIfReachGoal(transform.position);
        ControlMovingAnimation();
    }

    protected virtual void ControlMovingAnimation()
    {
        if (animator != null)
        {
            if (enemyMovementScript != null)
            {
                if (enemyMovementScript.baseMovingSpeed.CalculatedValue >= 0)
                {
                    if (enemyMovementScript.baseMovingSpeed.BaseValue == 0)
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, enemyMovementScript.baseMovingSpeed.CalculatedValue);
                    }
                    else
                    {
                        animator.SetFloat(movingAnimationSpeedMultHashID, enemyMovementScript.baseMovingSpeed.CalculatedValue/enemyMovementScript.baseMovingSpeed.BaseValue);
                    }
                }
            }
            else
            {
                animator.SetFloat(movingAnimationSpeedMultHashID, 0f);
            }
        }
    }

    public virtual void Die()
    {
        if (isDead || reachedGoal) { return; }
        OnDying();

        OnDied();
    }

    protected virtual void OnDied()
    {
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyDie(this));
        }
    }

    protected virtual void OnDying()
    {
        isDead = true;
        isHitable = false;
        if (hitCollider != null)
        {
            hitCollider.enabled = false;
        }
        OnEnemyDieEvent?.Invoke();
        AudioManager.PlayOneShotSound(audioSource, dieSFX);
        PlayDieAnimation();
    }

    protected virtual void PlayDieAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }

    public virtual void ReachGoal()
    {
        if (isDead || reachedGoal) { return; }
        OnReachingGoal();
        OnReachedGoal();
    }

    protected virtual void OnReachedGoal()
    {
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyReachedGoal(this));
        }
    }

    protected virtual void OnReachingGoal()
    {
        reachedGoal = true;
        isHitable = false;
        if (hitCollider != null)
        {
            hitCollider.enabled = false;
        }
        AudioManager.PlayOneShotSound(audioSource, attackSFX);
        PlayWinAniamtion();
    }

    protected virtual void PlayWinAniamtion()
    {
        if (animator != null)
        {
            animator.SetTrigger("Win");
        }
    }

    public virtual void GetHit(float damage)
    {
        if (!isHitable || isDead || reachedGoal) { return; }
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Die();
        }
        else
        {
            AudioManager.PlayOneShotSound(audioSource, damageSFX);
        }
    }
    protected virtual void CheckIfReachGoal(Vector3 pos)
    {
        if (pos.x == goal.GridPos.x && pos.z == goal.GridPos.y && !isDead)
        {
            if (!reachedGoal)
            {
                ReachGoal();
            }
        }
    }


    /// <summary>
    /// Using at Animation Clip
    /// </summary>
    public virtual void PlayMovingSound()
    {
        AudioManager.PlayOneShotSound(audioSource, movingSFX);
    }



}
