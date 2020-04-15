using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyMovement : MonoBehaviour, IEnemyMove
{
    [SerializeField] protected float movingSpeed = 5f;
    protected PathFinder pathFinder = null;
    protected bool isMovable = true;

    Enemy enemyMain;

    protected virtual void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        if (pathFinder == null)
        {
            Debug.LogError("Couldn't find PathFinder");
            return;
        }
    }

    protected virtual void OnDisable()
    {
        enemyMain.OnDieEvent -= StopMoving;
    }

    protected virtual void OnEnable()
    {
        enemyMain = GetComponent<Enemy>();
        if (enemyMain != null)
        {
            enemyMain.OnDieEvent += StopMoving;
        }
    }

    public abstract void MoveToGoal();

    protected virtual void StopMoving()
    {
        isMovable = false;
    }
}
