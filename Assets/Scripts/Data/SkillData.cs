using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkillData : ScriptableObject
{

    public List<SkillStruct> Data { get; private set; }

    [MenuItem("Assets/Data/Skill")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<SkillData>();
    }

    public List<SkillStruct> Load()
    {
        Data = new List<SkillStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/Skill"));
        foreach (Dictionary<string, object> line in csv)
        {
            SkillStruct skillStruct = new SkillStruct(
                int.Parse(line["cid"].ToString()),
                line["name"].ToString(),
                line["name_kor"].ToString(),
                line["animset"],
                line["attack_type"].ToString(),
                line["abnormal"].ToString()
            );
            Data.Add(skillStruct);
        }

        return Data;
    }

    public SkillStruct GetSkillStruct(int cid)
    {
        return Data.Find(s => s.cid == cid);
    }
    
}
