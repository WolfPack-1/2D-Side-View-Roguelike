using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LivingEntityData : ScriptableObject
{
    public List<LivingEntityStruct> Data { get; private set; }

    [MenuItem("Assets/Data/LivingEntity")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<LivingEntityData>();
    }

    public List<LivingEntityStruct> Load()
    {
        Data = CSVParser.LoadObjects<LivingEntityStruct>("PC.csv");
        return Data;
    }
}