using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExecuteEditor;

[CustomEditor(typeof(ExecuteEditorWaypointSnap))]
[CanEditMultipleObjects]
public class ExecuteEditorWaypointSnapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUI.enabled = false;
        SerializedProperty prop = serializedObject.FindProperty("m_Script");
        EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
        GUI.enabled = true;
        serializedObject.ApplyModifiedProperties();
        ExecuteEditorWaypointSnap instance = (ExecuteEditorWaypointSnap)target;
    }
}
