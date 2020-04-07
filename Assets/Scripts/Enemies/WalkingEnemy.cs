using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void GetHit(float damage)
    {
        healthPoint -= damage;
        if(healthPoint <= 0)
        {
            Die();
        }
    }

}
