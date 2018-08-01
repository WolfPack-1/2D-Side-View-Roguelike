using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class NPCTubeData : ScriptableObject
{
    public List<TubeStyleStruct> StyleData { get; private set; }
    public List<TubeCoolerStruct> CoolerData { get; private set; }
    public List<TubeEnhancerStruct> EnhancerData { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/NPCTube")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<NPCTubeData>();
    }
#endif

    public void LoadAll()
    {
        StyleData = LoadStyle();
        CoolerData = LoadCooler();
        EnhancerData = LoadEnhancer();
    }

    public List<TubeStyleStruct> LoadStyle()
    {
        StyleData = CSVParser.LoadObjects<TubeStyleStruct>("NPCTubeStyle");
        return StyleData;
    }

    public List<TubeCoolerStruct> LoadCooler()
    {
        CoolerData = CSVParser.LoadObjects<TubeCoolerStruct>("NPCTubeCooler");
        return CoolerData;
    }

    public List<TubeEnhancerStruct> LoadEnhancer()
    {
        EnhancerData = CSVParser.LoadObjects<TubeEnhancerStruct>("NPCTubeEnhancer");
        return EnhancerData;
    }


    public TubeStyleStruct FindStyleStruct(int cid)
    {
        return StyleData.Find(t => t.cid == cid);
    }
}