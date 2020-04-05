using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;
    [SerializeField] Color startWaypointColor = Color.red;
    [SerializeField] Color endWaypointColor = Color.green;
    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    readonly Vector2[] directions = new Vector2[]
    {
        Vector2.up, Vector2.down,
        Vector2.left, Vector2.right
    };



    void Awake()
    {
        LoadWaypoints();
        FindPathBFS();
        //StartCoroutine(PathFind());
    }

    private void FindPathBFS()
    {
        bool endFound = false;
        Queue<Waypoint> path = new Queue<Waypoint>();
        HashSet<Waypoint> visited = new HashSet<Waypoint>();
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();

        path.Enqueue(startWaypoint);
        while (path.Count > 0 && !endFound)
        {
            var current = path.Dequeue();
            visited.Add(current);
            ExploreNeighbours(ref endFound, path, visited, cameFrom, current);
        }
        if (!endFound)
        {
            Debug.LogWarning("Couldn't find end waypoint");
        }
        else
        {
            foreach (var wp in GetBFSPath(startWaypoint, endWaypoint, cameFrom))
            {
                SetWaypointTopColor(wp, Color.black);
                Debug.Log(wp);
            }
        }
    }

    private void ExploreNeighbours(ref bool endFound, Queue<Waypoint> path, HashSet<Waypoint> visited, Dictionary<Waypoint, Waypoint> cameFrom, Waypoint current)
    {
        foreach (var direction in directions)
        {
            grid.TryGetValue(current.GridPos + direction, out Waypoint next);
            if (next != null)
            {
                if (next == endWaypoint)
                {
                    cameFrom.Add(next, current);
                    endFound = true;
                    break;
                }
                if (!visited.Contains(next))
                {
                    path.Enqueue(next);
                    visited.Add(next);
                    cameFrom.Add(next, current);
                    SetWaypointTopColor(next, Color.yellow);
                }
            }
        }
    }

    List<Waypoint> GetBFSPath(Waypoint startWaypoint, Waypoint endWaypoint, Dictionary<Waypoint, Waypoint> cameFrom)
    {
        List<Waypoint> result = new List<Waypoint>();
        Waypoint current = endWaypoint;
        result.Add(endWaypoint);
        while (current != startWaypoint)
        {
            if (!cameFrom.TryGetValue(current, out current))
            {
                result = null;
                break;
            }
            result.Add(current);
        }
        if (result != null)
        {
            result.Reverse();
        }
        return result;
    }

    private bool CheckStartEndWaypoints()
    {
        if (startWaypoint == null)
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
        Vector2 startGridPos = startWaypoint.GridPos;
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
        if (startWaypoint == endWaypoint)
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
