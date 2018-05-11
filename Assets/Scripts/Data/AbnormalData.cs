using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AbnormalData : ScriptableObject
{
    public List<AbnormalStruct> Data { get; private set; }

    public AbnormalStruct GetAbnormal(int cid)
    {
        return Data.Find(d => d.cid == cid);
    }

    [MenuItem("Assets/Data/Abnormal")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<AbnormalData>();
    }

    public List<AbnormalStruct> Load()
    {
        Data = CSVParser.LoadObjects<AbnormalStruct>("Abnormal.csv");
        return Data;
    }
}