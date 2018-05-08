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
        Data = CSVParser.LoadObjects<GimmickStruct>("Gimmick.csv");
        return Data;
    }
    
}