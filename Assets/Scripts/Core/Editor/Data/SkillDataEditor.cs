using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    List<SkillStruct> data;
    
    void OnEnable()
    {
        data = Resources.Load<SkillData>("Data/ScriptableObject/Skill").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | name_kor | animset | attack_type | abnormal");
            
        GUILayout.EndHorizontal();
        
        foreach (SkillStruct skillStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(skillStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}
