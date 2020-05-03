using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] [Min(1f)] private const float gridSize = 1f;
    public float GridSize
    {
        get
        {
            return gridSize;
        }
    }

    public Vector2 GridPos
    {
        get
        {
            return new Vector2(
                Mathf.RoundToInt(transform.position.x / gridSize),
                Mathf.RoundToInt(transform.position.z / gridSize)
                );
        }
    }

    public void SetTopColor(Color color)
    {
        var top = transform.Find("Top");
        if (top != null)
        {
            var mesh = top.GetComponent<MeshRenderer>();
            if (mesh != null)
            {
                mesh.material.color = color;
            }
        }
    }
}
