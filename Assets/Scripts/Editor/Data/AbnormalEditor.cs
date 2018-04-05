using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbnormalData))]
public class AbnormalEditor : Editor
{

    List<AbnormalStruct> data;

    void OnEnable()
    {
        data = Resources.Load<AbnormalData>("Data/ScriptableObject/Abnormal").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | nameKor | abnormal_type | target");
            
        GUILayout.EndHorizontal();
        
        foreach (AbnormalStruct abnormalStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(abnormalStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}