using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] private int money;
    
    public event Action OnEnemyDieEvent;

    public bool isHitable = true;
    public bool isDead = false;
    public bool reachedGoal = false;


    protected Waypoint goal;

    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    public int Damage { get => damage; set => damage = value; }
    public int Money { get => money; protected set => money = value; }

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
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemySpawned(this));
        }
    }

    protected virtual void Update()
    {
        CheckIfReachGoal(transform.position);
    }

    public virtual void Die()
    {
        if(isDead || reachedGoal) { return; }
        isDead = true;
        isHitable = false;
        OnEnemyDieEvent?.Invoke(); 
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyDie(this));
        }
    }

    public virtual void ReachGoal()
    {
        if (isDead || reachedGoal) { return; }
        reachedGoal = true;
        isHitable = false;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyReachedGoal(this));
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
}
