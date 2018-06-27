using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Area))]
public class AreaEditor : Editor
{
    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawHandles(Area area, GizmoType gizmoType)
    {
        Handles.color = Color.green;
        switch (area.AreaMode)
        {
            case Area.AreaModeEnum.Box:
                Handles.DrawWireCube(area.transform.position, new Vector3(area.Size.x, area.Size.y));
                break;
            case Area.AreaModeEnum.Circle:
                Handles.DrawWireDisc(area.transform.position, Vector3.back, area.Radius);
                break;
        }
    }
}