using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AbnormalData : ScriptableObject
{
    public List<AbnormalStruct> Data { get; private set; }

    public AbnormalStruct GetAbnormal(int cid)
    {
        return Data.Find(d => d.cid == cid);
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Abnormal")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<AbnormalData>();
    }
#endif
    
    public List<AbnormalStruct> Load()
    {
        Data = CSVParser.LoadObjects<AbnormalStruct>("Abnormal");
        return Data;
    }
}