using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyMove
{
    [SerializeField] float movingTime = 5f;
    PathFinder pathFinder = null;
    public float speed = 5f;

    //Smooth moving
    //Queue<Waypoint> waypoints;
    //Waypoint target;
    //bool isAbleToMove = true;


    // Start is called before the first frame update

    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        if (pathFinder == null)
        {
            Debug.LogError("Couldn't find PathFinder");
            //isAbleToMove = false;
            return;
        }
        if (pathFinder.ShortestPathBFS == null)
        {
            Debug.LogError("Couldn't find Path to end point");
            //isAbleToMove = false;
            return;
        }

        MoveToGoal();

        //waypoints = new Queue<Waypoint>(pathFinder.ShortestPathBFS);
        //GetNextWaypoint();
    }
    
    // Update is called once per frame
    void Update()
    {
        //if (!isAbleToMove) { return; }
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
        //if (transform.position == target.transform.position)
        //{
        //    if (waypoints.Count > 0)
        //    {
        //        GetNextWaypoint();
        //    }
        //    else
        //    {
        //        isAbleToMove = false;
        //    }
        //}
    }

    IEnumerator FollowPath(List<Waypoint> path, float movingTime)
    {
        Debug.Log("Starting patrol...");
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movingTime / path.Count);
        }
        Debug.Log("Ending patrol");
    }

    public void MoveToGoal()
    {
        StartCoroutine(FollowPath(pathFinder.ShortestPathBFS, movingTime));
    }

    //private void GetNextWaypoint()
    //{
    //    target = waypoints.Dequeue();
    //    transform.LookAt(target.transform);
    //}
}
