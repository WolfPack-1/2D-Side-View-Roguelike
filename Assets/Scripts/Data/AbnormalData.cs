using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AbnormalData : ScriptableObject
{
    public List<AbnormalStruct> Data { get; private set; }

    [MenuItem("Assets/Data/Abnormal")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<AbnormalData>();
    }

    public List<AbnormalStruct> Load()
    {
        Data = new List<AbnormalStruct>();
        List<Dictionary<string, object>> csv = CSVParser.Read(Resources.Load<TextAsset>("Data/CSV/abnormal"));
        foreach (Dictionary<string, object> line in csv)
        {
            AbnormalStruct abnormalStruct = new AbnormalStruct(
                int.Parse(line["cid"].ToString()),
                line["name"].ToString(),
                line["name_kor"].ToString(),
                line["abnormal_Type"].ToString(),
                line["target"].ToString()
            );
            Data.Add(abnormalStruct);
        }

        return Data;
    }
}