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
        Data = CSVParser.LoadObjects<PlaceDivisionStruct>("PlaceDivision.csv");
        return Data;
    }
    
}