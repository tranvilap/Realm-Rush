using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint = null, endWaypoint = null;
    
    public Waypoint StartWaypoint { get => startWaypoint; set => startWaypoint = value; }
    public Waypoint EndWaypoint { get => endWaypoint; set => endWaypoint = value; }

}
