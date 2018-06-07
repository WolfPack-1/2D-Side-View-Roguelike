using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GimmickData))]
public class GimmickDataEditor : Editor
{

    List<GimmickStruct> data;
    
    void OnEnable()
    {
        data = Resources.Load<GimmickData>("Data/ScriptableObject/Gimmick").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("Cid | Name | FunctionType | FunctionValue");
            
        GUILayout.EndHorizontal();
        
        foreach (GimmickStruct gimmickStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(gimmickStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}
