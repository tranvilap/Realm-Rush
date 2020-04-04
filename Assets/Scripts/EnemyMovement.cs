using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path=null;
    [SerializeField] float movingTime=5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath(movingTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FollowPath(float movingTime)
    {
        Debug.Log("Starting patrol...");
        foreach(var waypoint in path)
        {
            Debug.Log("Visiting block: " + waypoint.transform.position);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movingTime/path.Count);
        }
        Debug.Log("Ending patrol");
    }
}
