using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FxData))]
public class FxDataEditor : Editor
{
    List<FxStruct> data;

    void OnEnable()
    {
        data = Resources.Load<FxData>("Data/ScriptableObject/Fx").Load();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("cid | name | nameKor | spriteName | upper | middleUpper | middle | middleDown | Down | backSide");
            
        GUILayout.EndHorizontal();
        
        foreach (FxStruct fxStruct in data)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(fxStruct.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}