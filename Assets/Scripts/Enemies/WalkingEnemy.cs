using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    bool reachedGoal = false;

    private void Update()
    {
        Vector3 pos = transform.position;
        CheckIfReachGoal(pos);
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }

    public override void GetHit(float damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Die();
        }
    }

    public override void ReachGoal()
    {
        base.ReachGoal();
        reachedGoal = true;
        Destroy(gameObject, 0.5f);
    }

    private void CheckIfReachGoal(Vector3 pos)
    {
        if (pos.x == endWaypoint.GridPos.x && pos.z == endWaypoint.GridPos.y)
        {
            if (!reachedGoal)
            {
                ReachGoal();
            }
        }
    }


}
