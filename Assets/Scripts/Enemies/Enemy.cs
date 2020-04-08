using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected float damage = 10f;
    protected Waypoint endWaypoint;

    public abstract void GetHit(float damage);
    public abstract void Die();

    protected virtual void ReachGoal()
    {
        Destroy(gameObject);
    }
}
