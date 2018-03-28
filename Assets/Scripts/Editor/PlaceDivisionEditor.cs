using UnityEditor;
using System.Collections.Generic;
using UnityEngine;


[CustomEditor(typeof(PlaceDivisionData))]
public class PlaceDivisionEditor : Editor
{
    List<PlaceDivisionStruct> data;
    
    void OnEnable()
    {
        data = Resources.Load<PlaceDivisionData>("Data/ScriptableObject/PlaceDivision").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("ID | Place | Name");
            
        GUILayout.EndHorizontal();
        
        foreach (PlaceDivisionStruct placeDivisionStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(placeDivisionStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}
