using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movingTime=5f;
    PathFinder pathFinder = null;
    // Start is called before the first frame update
    void Start()
    {
        pathFinder = FindObjectOfType<PathFinder>();
        if(pathFinder == null)
        {
            Debug.LogError("Couldn't find PathFinder");
            return;
        }
        if(pathFinder.ShortestPathBFS == null)
        {
            Debug.LogError("Couldn't find Path to end point");
            return;
        }
        StartCoroutine(FollowPath(pathFinder.ShortestPathBFS,movingTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath(List<Waypoint> path, float movingTime)
    {
        Debug.Log("Starting patrol...");
        foreach(var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movingTime/path.Count);
        }
        Debug.Log("Ending patrol");
    }
}
