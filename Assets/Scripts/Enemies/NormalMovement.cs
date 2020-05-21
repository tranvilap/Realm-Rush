using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NormalMovement : EnemyMovement
{
    [SerializeField] float arrivalDistance = 0.1f;

    Enemy enemy;
    private Rigidbody rb;
    int waypointIndex = 0;
    float lastDistanceToTarget = 0f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        enemy = GetComponent<Enemy>();

        Vector3 newPos = pathFinder.ShortestPathBFS[waypointIndex].transform.position;
        newPos.y = 0;
        transform.position = newPos;
        waypointIndex++;
        transform.LookAt(pathFinder.ShortestPathBFS[waypointIndex].transform);
        lastDistanceToTarget = Vector3.Distance(transform.position, pathFinder.ShortestPathBFS[waypointIndex].transform.position);
    }

    public override void MoveToGoal()
    {
        if (!isMovable) { return; }
        if (enemyScript.MoveSpeed.CalculatedValue <= 0) { return; }

        if (enemy != null)
        {
            if (enemy.isDead) { return; }
        }
        var targetWayPoint = pathFinder.ShortestPathBFS[waypointIndex];

        //If we're close to target, or overshot it, get next waypoint;
        float distanceToTarget = Vector3.Distance(transform.position, targetWayPoint.transform.position);
        if ((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
        {
            if (waypointIndex +1 >= pathFinder.ShortestPathBFS.Count)
            {
                isMovable = false;
                transform.position = pathFinder.ShortestPathBFS[waypointIndex].transform.position;
                return;
            }
            waypointIndex++;
            targetWayPoint = pathFinder.ShortestPathBFS[waypointIndex];
            lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.transform.position);

            transform.DOLookAt(targetWayPoint.transform.position, distanceToTarget/enemyScript.MoveSpeed.CalculatedValue);
        }
        else
        {
            lastDistanceToTarget = distanceToTarget;
        }

        //Get direction to the waypoint.
        //Normalize so it doesn't change with distance.
        Vector3 dir = (targetWayPoint.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + dir * (enemyScript.MoveSpeed.CalculatedValue * Time.fixedDeltaTime));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToGoal();
    }
}
