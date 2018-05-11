using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TubeData))]
public class TubeDataEditor : Editor
{
    List<TubeStruct> data;

    void OnEnable()
    {
        data = Resources.Load<TubeData>("Data/ScriptableObject/Tube").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | nameKor | socket | grade | company | motion | hitValue | attackType | position | damage | melee | range | bounce | instant | Splash | cooltime | abnormalValue | distance");
            
        GUILayout.EndHorizontal();
        
        foreach (TubeStruct tubeStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(tubeStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}