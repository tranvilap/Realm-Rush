using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float healthPoint = 10f;
    [SerializeField] protected int damage = 10;
    protected Waypoint endWaypoint;
    
    public abstract void GetHit(float damage);
    public abstract void Die();
    public abstract void ReachGoal();

    protected virtual void Start()
    {
        Map map = FindObjectOfType<Map>();
        if (map != null)
        {
            endWaypoint = map.EndWaypoint;
        }
    }
}
