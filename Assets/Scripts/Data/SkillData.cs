using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class SkillData : ScriptableObject
{

    public List<SkillStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Skill")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<SkillData>();
    }
#endif

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