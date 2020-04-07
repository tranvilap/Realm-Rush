using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExecuteEditor
{
    public class ExecuteEditorSnap : ExecuteEditorBase
    {
        [SerializeField] [Min(1f)] float gridSize = 1f;
        private Vector3 snapPos = new Vector3();
        // Update is called once per frame
        void Update()
        {
            SnapToGrid();
        }

        protected virtual void SnapToGrid()
        {
            snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
            snapPos.y = transform.position.y;
            snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
            transform.position = snapPos;
        }
    }
}