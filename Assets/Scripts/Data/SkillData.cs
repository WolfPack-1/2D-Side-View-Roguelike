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
        Data = CSVParser.LoadObjects<SkillStruct>("Skill.csv");
        return Data;
    }

    public SkillStruct GetStructByID(int cid)
    {
        return Data.Find(s => s.cid == cid);
    }
    
}
