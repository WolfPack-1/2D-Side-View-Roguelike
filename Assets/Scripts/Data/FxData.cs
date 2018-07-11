using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FxData : ScriptableObject
{

    public List<FxStruct> Data { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Fx")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<FxData>();
    }
#endif

    public List<FxStruct> Load()
    {
        Data = CSVParser.LoadObjects<FxStruct>("Fx.csv");
        return Data;
    }

    public FxStruct GetStructByID(int cid)
    {
        return Data.Find(s => s.cid == cid);
    }
    
}
