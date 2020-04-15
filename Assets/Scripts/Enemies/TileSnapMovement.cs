using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSnapMovement : EnemyMovement
{
    protected override void Start()
    {
        base.Start();
        MoveToGoal();
    }
    IEnumerator FollowPath(List<Waypoint> path, float movingTime)
    {
        foreach (var waypoint in path)
        {
            if (!isMovable) { break; }
            transform.LookAt(waypoint.transform);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movingTime);
        }
    }

    public override void MoveToGoal()
    {
        if (isMovable)
        {
            StartCoroutine(FollowPath(pathFinder.ShortestPathBFS, movingSpeed));
        }
    }
}
