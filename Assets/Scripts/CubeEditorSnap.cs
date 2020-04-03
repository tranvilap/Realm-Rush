using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditorSnap : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 snapPos = new Vector3();
        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSize) * gridSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / gridSize) * gridSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSize) * gridSize;
        transform.position = snapPos;
    }
}
