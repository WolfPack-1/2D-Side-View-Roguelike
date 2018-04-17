using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Area))]
public class AreaEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(Area area, GizmoType gizmoType)
    {
        Handles.color = Color.green;
        if(area.Size != Vector2Int.zero)
            Handles.DrawWireCube(area.transform.position, new Vector3(area.Size.x, area.Size.y));
        else if(area.Radius > 0)
            Handles.DrawWireDisc(area.transform.position, Vector3.back, area.Radius);
    }
}