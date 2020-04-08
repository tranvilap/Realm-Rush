using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyMovement : MonoBehaviour, IEnemyMove
{
    [SerializeField] protected float movingSpeed = 5f;
    protected PathFinder pathFinder = null;
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
}
