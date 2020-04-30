using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyMovement : MonoBehaviour, IEnemyMove
{
    [SerializeField] protected float baseMovingSpeed = 5f;
    protected PathFinder pathFinder = null;
    protected bool isMovable = true;

    Enemy enemyMain;

    protected virtual void OnEnable()
    {
        enemyMain = GetComponent<Enemy>();
        if (enemyMain != null)
        {
            enemyMain.OnEnemyDieEvent += StopMoving;
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
        enemyMain.OnEnemyDieEvent -= StopMoving;
    }
}
