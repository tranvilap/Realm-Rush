using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] WaypointPath[] paths = null;

    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    readonly Vector2[] directions = new Vector2[]
    {
            Vector2.up, Vector2.down,
            Vector2.left, Vector2.right
    };
    private void Awake()
    {
        LoadWaypoints();
        foreach (var path in paths)
        {
            path.path = FindPathBFS(path.StartWaypoint, path.EndWaypoint);
        }
    }

    private void LoadWaypoints()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

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

    }

    private List<Waypoint> FindPathBFS(Waypoint start, Waypoint end)
    {
        if (start == null || end == null) { return null; }
        if (start == end) { return new List<Waypoint> { start }; }
        bool endFound = false;
        Queue<Waypoint> path = new Queue<Waypoint>();
        HashSet<Waypoint> visited = new HashSet<Waypoint>();
        Dictionary<Waypoint, Waypoint> cameFrom = new Dictionary<Waypoint, Waypoint>();
        path.Enqueue(start);
        while (path.Count > 0 && !endFound)
        {
            var current = path.Dequeue();
            visited.Add(current);
            foreach (var direction in directions) //Explore neighbours
            {
                grid.TryGetValue(current.GridPos + direction, out Waypoint next);
                if (next != null)
                {
                    if (next == end)
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
                    }
                }
            }
        }
        if (endFound)
        {
            return BreadcrumbTrack(start, end, cameFrom);
        }
        return null;
    }

    List<Waypoint> BreadcrumbTrack(Waypoint startWaypoint, Waypoint endWaypoint, Dictionary<Waypoint, Waypoint> cameFrom)
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


    /// <summary>
    /// Get shortest path by index
    /// </summary>
    /// <param name="index">Path index (-1 for random path)</param>
    /// <returns>List of waypoints that combining to a path (return null if there is not a path)</returns>
    public WaypointPath GetPath(int index)
    {
        if (index >= paths.Length || index < -1) { return null; }
        if (index == -1)
        {

            return paths[UnityEngine.Random.Range(0, paths.Length)];
        }
        else
        {
            return paths[index];
        }
    }
}
[Serializable]
public class WaypointPath
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;
    [HideInInspector] public List<Waypoint> path = new List<Waypoint>();

    public Waypoint StartWaypoint { get => startWaypoint; }
    public Waypoint EndWaypoint { get => endWaypoint; }
    public WaypointPath() { }
    public WaypointPath(WaypointPath wpPath)
    {
        startWaypoint = wpPath.StartWaypoint;
        endWaypoint = wpPath.EndWaypoint;
        path = wpPath.path;
    }
}