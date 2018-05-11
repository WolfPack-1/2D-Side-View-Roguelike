using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TubeData : ScriptableObject
{
    public List<TubeStruct> Data { get; private set; }

    [MenuItem("Assets/Data/Tube")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<TubeData>();
    }

    public List<TubeStruct> Load()
    {
        Data = CSVParser.LoadObjects<TubeStruct>("Tube.csv");
        return Data;
    }
}