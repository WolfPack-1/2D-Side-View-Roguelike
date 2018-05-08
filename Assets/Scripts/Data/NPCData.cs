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
                line["animset"].ToString(),
                line["attack_type"].ToString(),
                int.Parse(line["attack_value"].ToString()),
                (TargetEnum) Enum.Parse(typeof(TargetEnum),line["attack_function"].ToString().ToUpper()),
                float.Parse(line["ATK"].ToString()),
                float.Parse(line["cooltime"].ToString()),
                line["skill"].ToString(),
                int.Parse(line["hp"].ToString()),
                (GradeEnum)Enum.Parse(typeof(GradeEnum),line["grade"].ToString().ToUpper()),
                (line["recognize"].ToString() == "1"),
                int.Parse(line["recognize_value"].ToString()),
                float.Parse(line["SPD"].ToString()),
                float.Parse(line["ATS"].ToString()),
                line["drop_table"].ToString(),
                float.Parse(line["DEF"].ToString())
            );
            Data.Add(npcStruct);
        }

        return Data;
    }

}