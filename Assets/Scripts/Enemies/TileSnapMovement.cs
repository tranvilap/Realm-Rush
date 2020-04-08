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
        Debug.Log("Starting patrol...");
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movingTime);
        }
        Debug.Log("Ending patrol");
    }

    public override void MoveToGoal()
    {
        StartCoroutine(FollowPath(pathFinder.ShortestPathBFS, movingSpeed));
    }
}
