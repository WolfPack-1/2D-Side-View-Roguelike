using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class TubeData : ScriptableObject
{
    public List<TubeStyleStruct> StyleData { get; private set; }
    public List<TubeCoolerStruct> CoolerData { get; private set; }
    public List<TubeEnhancerStruct> EnhancerData { get; private set; }
    public List<TubeRelicStruct> RelicData { get; private set; }

#if UNITY_EDITOR
    [MenuItem("Assets/Data/Tube")]
    public static void CreateAsset()
    {
        ScriptableObjectExtension.CreateAsset<TubeData>();
    }
#endif

    public void LoadAll()
    {
        StyleData = CSVParser.LoadObjects<TubeStyleStruct>("TubeStyle");
        CoolerData = CSVParser.LoadObjects<TubeCoolerStruct>("TubeCooler");
        EnhancerData = CSVParser.LoadObjects<TubeEnhancerStruct>("TubeEnhancer");
        RelicData = CSVParser.LoadObjects<TubeRelicStruct>("TubeRelic");
    }

    public List<TubeStyleStruct> LoadStyle()
    {
        if (StyleData != null)
            return StyleData;
        StyleData = CSVParser.LoadObjects<TubeStyleStruct>("TubeStyle");
        return StyleData;
    }

    public List<TubeCoolerStruct> LoadCooler()
    {
        if (CoolerData != null)
            return CoolerData;
        CoolerData = CSVParser.LoadObjects<TubeCoolerStruct>("TubeCooler");
        return CoolerData;
    }

    public List<TubeEnhancerStruct> LoadEnhancer()
    {
        if (EnhancerData != null)
            return EnhancerData;
        EnhancerData = CSVParser.LoadObjects<TubeEnhancerStruct>("TubeEnhancer");
        return EnhancerData;
    }

    public List<TubeRelicStruct> LoadRelic()
    {
        if (RelicData != null)
            return RelicData;
        RelicData = CSVParser.LoadObjects<TubeRelicStruct>("TubeRelic");
        return RelicData;
    }

    public TubeStyleStruct FindStyleStruct(int cid)
    {
        return StyleData.Find(t => t.cid == cid);
    }
}