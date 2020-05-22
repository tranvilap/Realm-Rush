using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NormalMovement : EnemyMovement
{
    [SerializeField] float arrivalDistance = 0.1f;

    private Rigidbody rb;
    int waypointIndex = 0;
    float lastDistanceToTarget = 0f;
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        enemyScript = GetComponent<Enemy>();
        if (movingPath != null)
        {
            if (movingPath.Count > 0)
            {
                waypointIndex++;
                transform.LookAt(movingPath[waypointIndex].transform);
                lastDistanceToTarget = Vector3.Distance(transform.position, movingPath[waypointIndex].transform.position);
            }
        }
    }

    public override void MoveToGoal()
    {
        if (!isMovable) { return; }
        if (base.enemyScript.MoveSpeed.CalculatedValue <= 0) { return; }
        if(movingPath == null) { return; }
        if(movingPath.Count <= 0) { return; }
        if (enemyScript != null)
        {
            if (enemyScript.isDead) { return; }
        }
        var targetWayPoint = movingPath[waypointIndex];

        //If we're close to target, or overshot it, get next waypoint;
        float distanceToTarget = Vector3.Distance(transform.position, targetWayPoint.transform.position);
        if ((distanceToTarget < arrivalDistance) || (distanceToTarget > lastDistanceToTarget))
        {
            if (waypointIndex + 1 >= movingPath.Count)
            {
                isMovable = false;
                transform.position = movingPath[waypointIndex].transform.position;
                return;
            }
            waypointIndex++;
            targetWayPoint = movingPath[waypointIndex];
            lastDistanceToTarget = Vector3.Distance(transform.position, targetWayPoint.transform.position);

            transform.DOLookAt(targetWayPoint.transform.position, distanceToTarget / base.enemyScript.MoveSpeed.CalculatedValue);
        }
        else
        {
            lastDistanceToTarget = distanceToTarget;
        }

        //Get direction to the waypoint.
        //Normalize so it doesn't change with distance.
        Vector3 dir = (targetWayPoint.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + dir * (base.enemyScript.MoveSpeed.CalculatedValue * Time.fixedDeltaTime));
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToGoal();
    }
}
