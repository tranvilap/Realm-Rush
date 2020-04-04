using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;
    [SerializeField] Color startWaypointColor = Color.red;
    [SerializeField] Color endWaypointColor = Color.green;
    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    // Start is called before the first frame update
    void Awake()
    {
        LoadWaypoints();
    }
    private bool CheckStartEndWaypoints()
    {
        if(startWaypoint == null)
        {
            Debug.LogError("Missing Start Waypoint", gameObject);
            return false;
        }
        if (endWaypoint == null)
        {
            Debug.LogError("Missing End Waypoint", gameObject);
            return false;
        }
        return true;
    }
    private void LoadWaypoints()
    {
        if (!CheckStartEndWaypoints()) { return; }
        var waypoints = FindObjectsOfType<Waypoint>();
        Vector2 startGridPos=startWaypoint.GridPos;
        Vector2 endGridPos = endWaypoint.GridPos;
        foreach (var waypoint in waypoints)
        {
            if (grid.ContainsKey(waypoint.GridPos))
            {
                Debug.LogWarning("Skipped overlapping block " + waypoint, waypoint);
                Destroy(waypoint.gameObject);
            }
            else
            {
                grid.Add(waypoint.GridPos, waypoint);
            }
        }

        //Check if Start and End Waypoints were loaded properly
        if (grid.ContainsKey(startGridPos))
        {
            //Set top's color for Start Waypoint
            grid.TryGetValue(startGridPos, out Waypoint start);
            SetWaypointTopColor(start, startWaypointColor);
            startWaypoint = start;
        }
        else
        {
            Debug.LogError("Couldn't get Start waypoint");
        }

        if (grid.ContainsKey(endGridPos))
        {
            //Set top's color for End Waypoint
            grid.TryGetValue(endGridPos, out Waypoint end);
            SetWaypointTopColor(end, endWaypointColor);
            endWaypoint = end;
        }
        else
        {
            Debug.LogError("Couldn't get End Waypoint");
        }
        //Check if Start Waypoint is the same as End Waypoint
        if(startWaypoint == endWaypoint)
        {
            Debug.LogError("Start Waypoint is the same as End Waypoint.", startWaypoint);
        }

        Debug.Log("Loaded " + grid.Count + "blocks.");
    }

    private void SetWaypointTopColor(Waypoint waypoint, Color color)
    {
        waypoint.SetTopColor(color);
    }


    // Update is called once per frame
    void Update()
    {

    }

}
