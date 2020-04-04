using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExecuteEditor
{
    [RequireComponent(typeof(Waypoint))]
    public class ExecuteEditorWaypointSnap : ExecuteEditorSnap
    {
        Waypoint waypoint;

        private void Awake()
        {
            waypoint = GetComponent<Waypoint>();
        }

        protected override void SnapToGrid()
        {
            Vector2 gridPos = waypoint.GridPos;
            float gridSize = waypoint.GridSize;
            //Vector z represent y gridPos position in the grid
            //Set Vector y to 0 to disable Y position;
            transform.position = new Vector3(gridPos.x * gridSize, 0f, gridPos.y * gridSize);
        }

    }
}
