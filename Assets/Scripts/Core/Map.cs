using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;
    [SerializeField] Color startWaypointColor = Color.red;
    [SerializeField] Color endWaypointColor = Color.green;

    public Waypoint StartWaypoint { get => startWaypoint; set => startWaypoint = value; }
    public Waypoint EndWaypoint { get => endWaypoint; set => endWaypoint = value; }
    public Color StartWaypointColor { get => startWaypointColor; set => startWaypointColor = value; }
    public Color EndWaypointColor { get => endWaypointColor; set => endWaypointColor = value; }

}
