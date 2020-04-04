using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExecuteEditor
{
    [RequireComponent(typeof(Waypoint))]
    public class ExecuteEditorCubePositionName : ExecuteEditorBase
    {
        Waypoint waypoint;
        private void Awake()
        {
            waypoint = GetComponent<Waypoint>();
        }
        // Update is called once per frame
        void Update()
        {
            UpdateNameByPosition();
        }

        private void UpdateNameByPosition()
        {
            Vector2 gridPos = waypoint.GridPos;
            gameObject.name = "X:" + gridPos.x + ", Y:" + gridPos.y;
        }
    }
}

