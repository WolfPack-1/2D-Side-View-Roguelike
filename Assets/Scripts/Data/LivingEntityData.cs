using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LivingEntityData : ScriptableObject
{
    public List<LivingEntityStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/LivingEntity")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<LivingEntityData>();
    }
#endif

    public List<LivingEntityStruct> Load()
    {
        Data = CSVParser.LoadObjects<LivingEntityStruct>("PC.csv");
        return Data;
    }
}