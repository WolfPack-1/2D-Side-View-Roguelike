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
                int.Parse(line["cid"].ToString()), 
                (string)line["name"], 
                (string)line["function_type"], 
                int.Parse(line["function_value"].ToString())
            );
            Data.Add(gimmickStruct);    
        }

        return Data;
    }
    
}