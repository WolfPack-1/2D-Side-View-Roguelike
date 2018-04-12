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
        Data = new List<LivingEntityStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/PC"));
        foreach (Dictionary<string, object> line in csv)
        {
            LivingEntityStruct livingEntityStruct = new LivingEntityStruct(
                int.Parse(line["cid"].ToString()),
                line["name"].ToString(),
                line["name_kor"].ToString(),
                float.Parse(line["HP"].ToString()),
                float.Parse(line["ATK"].ToString()),
                float.Parse(line["DEF"].ToString()),
                float.Parse(line["ATS"].ToString()),
                float.Parse(line["SPD"].ToString())
                );
            Data.Add(livingEntityStruct);
        }

        return Data;
    }
}