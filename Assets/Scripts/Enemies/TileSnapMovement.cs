using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TileSnapMovement : EnemyMovement
{
    int pathIndex = -1;
    float timeEachStep = 0f;
    float timer = 0f;

    private void Update()
    {
        MoveToGoal();
    }
    public override void MoveToGoal()
    {
        if (!isMovable) { return; }
        if (enemyScript.MoveSpeed.CalculatedValue > 0)
        {
            timeEachStep = 1 / enemyScript.MoveSpeed.CalculatedValue;
        }
        if(pathIndex < 0) { timer = timeEachStep; }
        if (timer < timeEachStep) { timer += Time.deltaTime; }
        else
        {
            if (pathIndex + 1 >= movingPath.Count) { isMovable = false; }
            else
            {
                pathIndex++;
                transform.position = movingPath[pathIndex].transform.position;
                if (pathIndex + 1 < movingPath.Count)
                {
                    Waypoint wp = movingPath[pathIndex + 1];
                    //transform.LookAt(pathFinder.ShortestPathBFS[pathIndex + 1].transform);
                    transform.DOLookAt(wp.transform.position, timeEachStep);
                }
                timer = 0;
            }
        }
    }
}
