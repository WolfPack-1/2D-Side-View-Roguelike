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
        Data = new List<NPCStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/NPC"));
        foreach (Dictionary<string, object> line in csv)
        {
            NPCStruct npcStruct = new NPCStruct(
                int.Parse(line["cid"].ToString()),
                line["name"].ToString(),
                line["name_kor"].ToString(),
                line["animset"],
                line["attack_type"].ToString(),
                int.Parse(line["attack_value"].ToString()),
                line["attack_function"].ToString(),
                float.Parse(line["attack_damage"].ToString()),
                float.Parse(line["cooltime"].ToString()),
                line["skill"],
                int.Parse(line["hp"].ToString()),
                (GradeEnum)Enum.Parse(typeof(GradeEnum),line["grade"].ToString()),
                bool.Parse(line["recognize"].ToString()),
                int.Parse(line["recognize_value"].ToString())
            );
            Data.Add(npcStruct);
        }

        return Data;
    }

}