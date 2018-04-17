using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Area))]
public class AreaEditor : Editor
{
    
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(Area area, GizmoType gizmoType)
    {
        Handles.DrawWireCube(area.transform.position, new Vector3(area.Size.x, area.Size.y));
    }
}