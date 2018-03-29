using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaceDivisionData : ScriptableObject
{
    public List<PlaceDivisionStruct> Data { get; private set; }

    [MenuItem("Assets/Data/PlaceDivision")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<PlaceDivisionData>();
    }

    public List<PlaceDivisionStruct> Load()
    {
        Data = new List<PlaceDivisionStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/Place_Division"));
        foreach (Dictionary<string, object> line in csv)
        {
            PlaceDivisionStruct placeDivisionStruct = new PlaceDivisionStruct
            (
                (int)line["ID"],
                (string)line["Place"],
                (string)line["Name"]
            );
            Data.Add(placeDivisionStruct);
        }

        return Data;
    }
    
}