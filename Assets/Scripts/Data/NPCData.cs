using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
      #endif


public class NPCData : ScriptableObject
{

    public List<NPCStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/NPC")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCData>();
    }
#endif

    public List<NPCStruct> Load()
    {
        Data = CSVParser.LoadObjects<NPCStruct>("NPC.csv");
        return Data;
    }

}