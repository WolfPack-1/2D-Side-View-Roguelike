using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCData : ScriptableObject
{

    public List<NPCStruct> Data { get; private set; }

    [MenuItem("Assets/Data/NPC")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCData>();
    }
    
    public List<NPCStruct> Load()
    {
        Data = CSVParser.LoadObjects<NPCStruct>("NPC.csv");
        return Data;
    }

}