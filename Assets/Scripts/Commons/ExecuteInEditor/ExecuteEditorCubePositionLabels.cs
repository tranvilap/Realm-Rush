using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExecuteEditor
{
    [RequireComponent(typeof(Waypoint))]
    public class ExecuteEditorCubePositionLabels : ExecuteEditorBase
    {
        [SerializeField] TextMesh xzLabelTextMesh = null;
        Waypoint waypoint=null;
        private void Awake()
        {
            waypoint = GetComponent<Waypoint>();
        }
        // Update is called once per frame
        void Update()
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            if (xzLabelTextMesh != null)
            {
                Vector2 gridPos = waypoint.GridPos;
                xzLabelTextMesh.text = gridPos.x + "," + gridPos.y;
            }
        }

        private void OnDisable()
        {
            if(xzLabelTextMesh != null)
            {
                xzLabelTextMesh.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            if (xzLabelTextMesh != null)
            {
                xzLabelTextMesh.gameObject.SetActive(true);
            }
        }
    }
}

