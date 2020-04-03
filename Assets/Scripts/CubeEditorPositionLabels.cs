using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditorPositionLabels : MonoBehaviour
{
    [SerializeField] TextMesh xzLabelTextMesh = null;
    [SerializeField] TextMesh yLabelTextMesh = null;

    // Update is called once per frame
    void Update()
    {
        if (xzLabelTextMesh != null)
        {
            xzLabelTextMesh.text = transform.position.x + "," + transform.position.z;
        }
        if (yLabelTextMesh != null)
        {
            yLabelTextMesh.text = transform.position.y.ToString();
        }
    }
}
