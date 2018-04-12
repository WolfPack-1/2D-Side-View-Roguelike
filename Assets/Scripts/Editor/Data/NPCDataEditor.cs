using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCData))]
public class NPCDataEditor : Editor
{

    List<NPCStruct> data;
    
    void OnEnable()
    {
        data = Resources.Load<NPCData>("Data/ScriptableObject/Npc").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | nameKor | animSet | attackType | attackValue | attackFunction | attackDamage | coolTime | skill | hp | grade | recognize | recognizeValue");
            
        GUILayout.EndHorizontal();
        
        foreach (NPCStruct npcStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(npcStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}
