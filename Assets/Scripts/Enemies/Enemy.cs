﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected ParticleSystem dieParticle = null;
    [SerializeField] protected ParticleSystem reachedGoalParticle = null;

    public bool isHitable = true;
    public bool isDead = false;

    protected Waypoint endWaypoint;

    public float HealthPoint { get => healthPoint; set => healthPoint = value; }
    public int Damage { get => damage; set => damage = value; }

    protected virtual void Start()
    {
        Map map = FindObjectOfType<Map>();
        if (map != null)
        {
            endWaypoint = map.EndWaypoint;
        }
    }

    public virtual void Die()
    {
        isDead = true;
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyDie(this));
        }
    }

    public virtual void ReachGoal()
    {
        foreach (var go in EventSystemListener.main.Listeners)
        {
            ExecuteEvents.Execute<IEnemyEvent>(go, null, (x, y) => x.OnEnemyReachedGoal(this));
        }
    }

    public abstract void GetHit(float damage);

}
