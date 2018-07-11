using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GimmickData : ScriptableObject
{

    public List<GimmickStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Gimmick")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<GimmickData>();

    }
#endif

    public List<GimmickStruct> Load()
    {
        Data = CSVParser.LoadObjects<GimmickStruct>("Gimmick.csv");
        return Data;
    }

}