﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] private int money;

    [SerializeField] protected ParticleSystem dieParticle = null;
    [SerializeField] protected ParticleSystem reachedGoalParticle = null;
    
    public event Action OnEnemyDieEvent;

    public bool isHitable = true;
    public bool isDead = false;
    public bool atGoal = false;

    protected Waypoint endWaypoint;

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
            endWaypoint = map.EndWaypoint;
        }
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemySpawned(this));
        }
    }

    public virtual void Die()
    {
        isDead = true;
        OnEnemyDieEvent?.Invoke(); 
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyDie(this));
        }
    }

    public virtual void ReachGoal()
    {
        atGoal = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyReachedGoal(this));
        }
    }

    public abstract void GetHit(float damage);

}
