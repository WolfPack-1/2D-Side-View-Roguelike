using UnityEditor;
using System.Collections.Generic;
using UnityEngine;


[CustomEditor(typeof(LivingEntityData))]
public class LivingEntityDataEditor : Editor
{
    List<LivingEntityStruct> data;
    
    void OnEnable()
    {
        data = Resources.Load<LivingEntityData>("Data/ScriptableObject/LivingEntity").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | nameKor | hp | atk | def | ats | spd");
            
        GUILayout.EndHorizontal();
        
        foreach (LivingEntityStruct playerStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(playerStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}
