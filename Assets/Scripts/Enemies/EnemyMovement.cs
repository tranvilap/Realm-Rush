using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyMovement : MonoBehaviour
{
    protected PathFinder pathFinder = null;
    protected bool isMovable = true;

    protected Enemy enemyScript;

    protected virtual void OnEnable()
    {
        enemyScript = GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.OnEnemyDieEvent += StopMoving;
        }
    }

    protected virtual void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        if (pathFinder == null)
        {
            Debug.LogError("Couldn't find PathFinder");
            return;
        }
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
