using Game.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Stats;
public class Enemy : MonoBehaviour
{
    [SerializeField] private BaseStat maxHP;
    [SerializeField] private BaseStat moveSpeed;
    [SerializeField] protected int damage = 10;
    [SerializeField] private int money;

    [SerializeField] HealthBar healthBar = null;
    [SerializeField][Tooltip("To get location for laser")] Transform body = null;

    [Header("SFX")]
    [SerializeField] SFXObj movingSFX = null;
    [SerializeField] SFXObj damageSFX = null;
    [SerializeField] SFXObj dieSFX = null;
    [SerializeField] SFXObj attackSFX = null;

    [SerializeField] [Tooltip("Prevent playing every frame (Ex: Laser hit)")] float audioDelay = 0.1f;
    public event Action OnEnemyDieEvent;

    private float currentHP;
    public bool isHitable = true;
    public bool isDead = false;
    public bool reachedGoal = false;

    private WaypointPath goalPath;

    public float CurrentHP { get => currentHP;
        set
        {
            if(value <= 0)
            {
                currentHP = 0;
            }
            else if(value >= MaxHP.CalculatedValue)
            {
                currentHP = MaxHP.CalculatedValue;
            }
            else
            {
                currentHP = value;
            }
        }
    }
    public int Damage { get => damage; set => damage = value; }
    public int Money { get => money; protected set => money = value; }
    public BaseStat MaxHP { get => maxHP; set => maxHP = value; }
    public BaseStat MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public Transform Body { get => body; set => body = value; }
    public WaypointPath GoalPath { get => goalPath; }

    protected Collider hitCollider;
    protected AudioSource audioSource;
    
    private bool isCoolingDownAudioDelay = false;
    private float audioDelayCounter = 0f;
    
    protected virtual void Awake()
    {
        var parent = GameObject.Find("Enemies");
        if (parent == null)
        {
            parent = new GameObject("Enemies");
            parent.transform.position = Vector3.zero;
        }
        transform.parent = parent.transform;

        currentHP = MaxHP.CalculatedValue;
    }

    protected virtual void Start()
    {
        hitCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHP.CalculatedValue);
            healthBar.SetHealth(CurrentHP);
        }
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemySpawned(this));
        }
    }

    protected virtual void Update()
    {
        if (isCoolingDownAudioDelay)
        {
            audioDelayCounter += Time.deltaTime;
            if (audioDelayCounter >= audioDelay)
            {
                isCoolingDownAudioDelay = false;
                audioDelayCounter = 0f;
            }
        }
        CheckIfReachGoal(transform.position);
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
    }



    public virtual void GetHit(float damage)
    {
        if (!isHitable || isDead || reachedGoal) { return; }
        CurrentHP -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(CurrentHP);
        }
        if (CurrentHP <= 0)
        {
            Die();
        }
        else
        {
            if (!isCoolingDownAudioDelay)
            {
                AudioManager.PlayOneShotSound(audioSource, damageSFX);
                isCoolingDownAudioDelay = true;
            }
        }
    }
    protected virtual void CheckIfReachGoal(Vector3 pos)
    {
        if (pos.x == GoalPath.EndWaypoint.GridPos.x && pos.z == GoalPath.EndWaypoint.GridPos.y && !isDead)
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

    public void SetPath(WaypointPath path)
    {
        this.goalPath = new WaypointPath(path);
    }

}
