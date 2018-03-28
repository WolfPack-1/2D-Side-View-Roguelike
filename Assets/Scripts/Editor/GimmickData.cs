using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GimmickData : ScriptableObject
{
    
    public List<GimmickStruct> Data { get; private set; }
    
    [MenuItem("Assets/Data/Gimmick")]
    public static void CreateAsset ()
    {
        ScriptableObjectExtension.CreateAsset<GimmickData> ();

    }

    public List<GimmickStruct> Load()
    {
        Data = new List<GimmickStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/Gimmick"));
        foreach (Dictionary<string, object> line in csv)
        {
            GimmickStruct gimmickStruct = new GimmickStruct(
                (int)line["cid"], 
                (string)line["name"], 
                (string)line["function_type"], 
                (int)line["function_value"]
            );
            Data.Add(gimmickStruct);    
        }

        return Data;
    }
    
}

[Serializable]
public struct GimmickStruct
{
    public int cid;
    public string name;
    public string functionType;
    public int functionValue;

    public GimmickStruct(int cid, string name, string functionType, int functionValue)
    {
        this.cid = cid;
        this.name = name;
        this.functionType = functionType;
        this.functionValue = functionValue;
    }

    public override string ToString()
    {
        return string.Format("{0} | {1} | {2} | {3}", cid, name, functionType, functionType);
    }
}