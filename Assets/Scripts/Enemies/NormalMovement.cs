using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMovement : EnemyMovement
{
    private Rigidbody rb;
    int counter = 0;
    public float distance = 2.0f; //on which distance you want to switch to the next waypoint

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    public override void MoveToGoal()
    {
        var direction = Vector3.zero;
        //get the vector from your position to current waypoint
        direction = pathFinder.ShortestPathBFS[counter].transform.position - transform.position;
        //check our distance to the current waypoint, Are we near enough?
        if (direction.magnitude < distance)
        {
            if (counter < pathFinder.ShortestPathBFS.Count - 1) //switch to the nex waypoint if exists
            {
                counter++;
            }
        }
        direction = direction.normalized;
        Vector3 dir = direction;

       rb.velocity = new Vector2(direction.x * baseMovingSpeed, direction.y * baseMovingSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        MoveToGoal();
    }
}
