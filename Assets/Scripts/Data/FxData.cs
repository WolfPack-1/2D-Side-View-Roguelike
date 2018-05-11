using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FxData : ScriptableObject
{

    public List<FxStruct> Data { get; private set; }

    [MenuItem("Assets/Data/Fx")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<FxData>();
    }

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
