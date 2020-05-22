using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyMovement : MonoBehaviour
{
    protected bool isMovable = true;

    protected Enemy enemyScript;
    protected List<Waypoint> movingPath = null;

    protected virtual void OnEnable()
    {
        if (enemyScript == null)
        {
            enemyScript = GetComponent<Enemy>();
        }
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDieEvent += StopMoving;
        }
    }

    protected virtual void Start()
    {
        movingPath = enemyScript.GoalPath.path;
    }

    public abstract void MoveToGoal();

    protected virtual void StopMoving()
    {
        isMovable = false;
    }

    protected virtual void OnDisable()
    {
        enemyScript.OnEnemyDieEvent -= StopMoving;
    }
}
