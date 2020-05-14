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
        if (baseMovingSpeed > 0)
        {
            timeEachStep = 1 / baseMovingSpeed;
        }
        if(pathIndex < 0) { timer = timeEachStep; }
        if (timer < timeEachStep) { timer += Time.deltaTime; }
        else
        {
            if (pathIndex + 1 >= pathFinder.ShortestPathBFS.Count) { isMovable = false; }
            else
            {
                pathIndex++;
                transform.position = pathFinder.ShortestPathBFS[pathIndex].transform.position;
                if (pathIndex + 1 < pathFinder.ShortestPathBFS.Count)
                {
                    Waypoint wp = pathFinder.ShortestPathBFS[pathIndex + 1];
                    //transform.LookAt(pathFinder.ShortestPathBFS[pathIndex + 1].transform);
                    transform.DOLookAt(wp.transform.position, timeEachStep);
                }
                timer = 0;
            }
        }
    }
}
