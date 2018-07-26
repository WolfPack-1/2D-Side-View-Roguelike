using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class PlaceDivisionData : ScriptableObject
{
    public List<PlaceDivisionStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/PlaceDivision")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<PlaceDivisionData>();
    }
#endif

    public List<PlaceDivisionStruct> Load()
    {
        Data = CSVParser.LoadObjects<PlaceDivisionStruct>("PlaceDivision");
        return Data;
    }

}