using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TubeData))]
public class TubeDataEditor : Editor
{
    List<TubeStyleStruct> styleData;
    List<TubeEnhancerStruct> enhancerData;
    List<TubeCoolerStruct> coolerData;
    List<TubeRelicStruct> relicData;
    
    void OnEnable()
    {
        styleData = Resources.Load<TubeData>("Data/ScriptableObject/Tube").LoadStyle();
        enhancerData = Resources.Load<TubeData>("Data/ScriptableObject/Tube").LoadEnhancer();
        coolerData = Resources.Load<TubeData>("Data/ScriptableObject/Tube").LoadCooler();
        relicData = Resources.Load<TubeData>("Data/ScriptableObject/Tube").LoadRelic();

    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox ("수정은 CSV파일의 직접 변경을 통해 가능합니다.", MessageType.Info);
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("Style");
            
        GUILayout.EndHorizontal();
        
        foreach (TubeStyleStruct data in styleData)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(data.ToString());
            
            GUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("Enhancer");
            
        GUILayout.EndHorizontal();
        
        foreach (TubeEnhancerStruct data in enhancerData)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(data.ToString());
            
            GUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("Cooler");
            
        GUILayout.EndHorizontal();
        
        foreach (TubeCoolerStruct data in coolerData)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(data.ToString());
            
            GUILayout.EndHorizontal();
        }
        
        EditorGUILayout.Space ();
        
        GUILayout.BeginHorizontal("BOX");
            
        EditorGUILayout.LabelField("Relic");
            
        GUILayout.EndHorizontal();
        
        foreach (TubeRelicStruct data in relicData)
        {
            GUILayout.BeginHorizontal("BOX");
            
            EditorGUILayout.LabelField(data.ToString());
            
            GUILayout.EndHorizontal();
        }
    }
}